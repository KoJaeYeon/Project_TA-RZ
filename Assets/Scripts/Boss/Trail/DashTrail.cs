using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrail : MonoBehaviour
{
    private float _trailSpeed = 10f / 3f;

    private Transform _defaultTr;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _defaultTr = transform.parent;
        transform.position = _defaultTr.position;
        transform.rotation = _defaultTr.rotation;
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
