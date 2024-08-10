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
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
        _input = player.GetComponent<PlayerInputSystem>();
        SetPlayerCamera();
    }

    private void Update()
    {
        if(_input.IsLockOn)
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

    void SetPlayerCamera()
    {
        CinemachineBrain cineBrain = Camera.main.GetComponent<CinemachineBrain>();
        var virutalCamera = cineBrain.ActiveVirtualCamera;
        virutalCamera.Follow = transform;
        virutalCamera.LookAt = transform;
    }
}
