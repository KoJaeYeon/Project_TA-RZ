using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraRoot : MonoBehaviour
{
    Quaternion rotation;
    [Inject] Player player;
    PlayerInputSystem _input;

    [SerializeField] Transform Target;
    bool beforeLockOnMode;
    void Start()
    {        
        _input = player.GetComponent<PlayerInputSystem>();
        beforeLockOnMode = _input.IsLockOn;
        SetCameraToPlayer();
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
            if (_input.IsLockOn)
            {
                SetCameraToTarget();
            }
            else
            {
                SetCameraToPlayer();
            }

            //카메라 입력값 초기화
            _input.DeltaLook = 0;

            beforeLockOnMode = _input.IsLockOn;
        }
    }

    void UpdateCameraRootTransform()
    {
        if (_input.IsLockOn)
        {
            transform.LookAt(Target);
        }
        else
        {
            CalculateTargetRotate();
            transform.rotation = rotation;
        }
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
        CinemachineBrain cineBrain = Camera.main.GetComponent<CinemachineBrain>();
        var virutalCamera = cineBrain.ActiveVirtualCamera;
        virutalCamera.Follow = transform;
        virutalCamera.LookAt = transform;
    }

    /// <summary>
    ///타켓에게 카메라 전환하는 함수
    /// </summary>
    void SetCameraToTarget()
    {
        CinemachineBrain cineBrain = Camera.main.GetComponent<CinemachineBrain>();
        var virutalCamera = cineBrain.ActiveVirtualCamera;
        virutalCamera.LookAt = Target.transform;
    }
}
