using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.UI.GridLayoutGroup;

public class CameraRoot : MonoBehaviour
{
    Quaternion rotation;
    [Inject] Player player;
    PlayerInputSystem _input;

    public CinemachineVirtualCamera cinemachineVirtualCamera { get; private set; }

    Transform _target;
    bool beforeLockOnMode;
    void Start()
    {        
        _input = player.GetComponent<PlayerInputSystem>();
        beforeLockOnMode = _input.IsLockOn;
        SetCameraToPlayer();
        Cursor.lockState = CursorLockMode.Locked;
        CinemachineBrain cineBrain = Camera.main.GetComponent<CinemachineBrain>();
        var virutalCamera = cineBrain.ActiveVirtualCamera;
        cinemachineVirtualCamera = virutalCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
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
        }
        else
        {
            if (SelectTarget() == false)
            {
                SetCameraToPlayer();
            }
        }
    }
}
