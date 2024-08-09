using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoot : MonoBehaviour
{
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        rot = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = rot;
    }
}
