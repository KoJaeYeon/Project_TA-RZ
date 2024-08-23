using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraRoot : MonoBehaviour
{
    Quaternion rotation;
    [Inject] Player player;
    PlayerInputSystem _input;

    [Inject] public CinemachineVirtualCamera cinemachineVirtualCamera { get; }

    Transform _target;
    bool beforeLockOnMode;

    #region ZoomIn/Out
    Coroutine _zoomCoroutine;
    [Header("Zoom")]
    [SerializeField] bool startZoomOut;

    [Header("ZoomOutSpeed")]
    [SerializeField] float _zoomOutspeed;

    [Header("ZoomInSpeed")]
    [SerializeField] float _zoomInspeed;

    float _time;
    float _cameraFieldOfView;
    float _maxView;
    #endregion
    #region lockOn
    [SerializeField] Image _lockOnUI;
    #endregion

    private void Awake()
    {
        _lockOnUI.gameObject.SetActive(false);
    }

    void Start()
    {        
        _input = player.GetComponent<PlayerInputSystem>();
        beforeLockOnMode = _input.IsLockOn;
        SetCameraToPlayer();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CheckLockOnModeChange();
        UpdateCameraRootTransform();
    }

    /// <summary>
    /// LockOnMode가 변화했으면 타겟을 설정
    /// </summary>
    void CheckLockOnModeChange()
    {
        if (beforeLockOnMode != _input.IsLockOn)
        {
            beforeLockOnMode = _input.IsLockOn;
            if (_input.IsLockOn && SelectTarget() == true)
            {
                SetCameraToTarget();
            }
            else
            {
                SetCameraToPlayer();
                _lockOnUI.gameObject.SetActive(false);
            }

            //카메라 입력값 초기화
            _input.DeltaLook = 0;


        }
    }

    void UpdateCameraRootTransform()
    {
        if (_input.IsLockOn)
        {
            CheckTargetAndSetCameraTarget();
        }
        else
        {
            CalculateTargetRotate();
            transform.rotation = rotation;
        }
    }

    /// <summary>
    /// Monster Layer 검사해서 타켓 설정해주는 메서드
    /// 타겟이 존재하지 않으면 false 반환
    /// </summary>
    /// <returns></returns>
    bool SelectTarget()
    {
        var colliders = Physics.OverlapSphere(transform.position, 6f, LayerMask.GetMask("Monster"));
        if(colliders.Length == 0)
        {
            return false;
        }

        float closestDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            float targetDistance = Vector3.Distance(collider.transform.position, transform.position);
            if (targetDistance < closestDistance)
            {
                _target = collider.transform;
                closestDistance = targetDistance;
            }
        }

        SetCameraToTarget();

        return true;

    }

    /// <summary>
    /// 마우스 Delta값을 받아 카메라를 회전시키는 함수
    /// </summary>
    void CalculateTargetRotate()
    {
        Vector3 currentEulerAngles = rotation.eulerAngles;
        currentEulerAngles.y += _input.DeltaLook;
        rotation.eulerAngles = currentEulerAngles;

        _input.DeltaLook = 0;
    }

    /// <summary>
    /// 플레이어에게 카메라 전환하는 함수
    /// </summary>
    void SetCameraToPlayer()
    {
        rotation = transform.rotation;
        cinemachineVirtualCamera.Follow = transform;
        cinemachineVirtualCamera.LookAt = transform;
        _input.SetLockOn(false);
    }

    /// <summary>
    ///타켓에게 카메라 전환하는 함수
    /// </summary>
    void SetCameraToTarget()
    {
        CinemachineBrain cineBrain = Camera.main.GetComponent<CinemachineBrain>();
        var virutalCamera = cineBrain.ActiveVirtualCamera;
        virutalCamera.LookAt = _target.transform;
        _lockOnUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// 타켓이 죽었는지 확인하고 검색되는 타겟이 없으면 플레이어로 재설정 해주는 함수
    /// 생존한 타켓이 있으면 타켓을 향해 바라본다.
    /// </summary>
    void CheckTargetAndSetCameraTarget()
    {
        if (_target.gameObject.activeSelf == true)
        {
            transform.LookAt(_target);
            Vector3 rot = transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.eulerAngles = rot;
            UpdateLockOnUI();
        }
        else
        {
            if (SelectTarget() == false)
            {
                SetCameraToPlayer();
                _lockOnUI.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateLockOnUI()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_target.position + Vector3.up);
        _lockOnUI.transform.position = screenPos;

    }

    public void StartCameraMovement()
    {
        startZoomOut = true;

        _zoomCoroutine = StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomOut()
    {
        _maxView = 90f;
        _cameraFieldOfView = cinemachineVirtualCamera.m_Lens.FieldOfView;

        _time = 0f;
        
        while (cinemachineVirtualCamera.m_Lens.FieldOfView < _maxView)
        {
            _time += _zoomOutspeed * Time.deltaTime;

            cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_cameraFieldOfView, _maxView, _time * _time);

            yield return null;
        }

        startZoomOut = false;
    }

    public void EndCameraMovement()
    {
        if(_zoomCoroutine != null)
        {
            StopCoroutine(_zoomCoroutine);
            startZoomOut = false;
        }

        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        if (startZoomOut)
        {
            yield return new WaitWhile(() => startZoomOut);
        }

        _time = 0f;

        float currentView = cinemachineVirtualCamera.m_Lens.FieldOfView;

        while (cinemachineVirtualCamera.m_Lens.FieldOfView > _cameraFieldOfView)
        {
            _time += _zoomInspeed * Time.deltaTime;

            cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(currentView, _cameraFieldOfView, _time * _time);

            yield return null;
        }
    }

}
