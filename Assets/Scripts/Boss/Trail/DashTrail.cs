using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrail : MonoBehaviour
{
    private float _rushSpeed = 10f;
    private float _waitTime = 3f;
    private float _trailSpeed;

    private Transform _defaultTr;

    private void Awake()
    {
        _trailSpeed = _rushSpeed / _waitTime;
    }

    private void OnEnable()
    {
        _defaultTr = transform.parent;
        transform.position = _defaultTr.position;
        transform.rotation = _defaultTr.rotation;
        Vector3 rotation = transform.eulerAngles;
        rotation.x = -0.1f;
        transform.eulerAngles = rotation;
        Invoke(nameof(DisableTrail), 3f);
        GetComponent<TrailRenderer>().Clear();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _trailSpeed * Time.deltaTime);
    }

    private void DisableTrail()
    { 
        gameObject.SetActive(false);
    }
}
