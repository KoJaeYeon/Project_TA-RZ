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
        _defaultTr = GetComponentInParent<Transform>();
        transform.position = _defaultTr.position;
        transform.rotation = _defaultTr.rotation;
        Invoke(nameof(DisableTrail), 3f);
    }

    private void Update()
    {
        transform.Translate(transform.forward * _trailSpeed * Time.deltaTime);
    }

    private void DisableTrail()
    { 
        gameObject.SetActive(false);
    }
}
