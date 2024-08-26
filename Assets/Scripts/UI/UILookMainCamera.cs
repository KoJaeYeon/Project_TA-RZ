using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookMainCamera : MonoBehaviour
{
    Transform _cameraTrans;
    private void Awake()
    {
        _cameraTrans = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_cameraTrans);
        transform.Rotate(0, 180, 0);
    }
}
