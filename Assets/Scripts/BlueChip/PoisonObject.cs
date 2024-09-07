using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoisonObject : MonoBehaviour
{
    [Inject]
    private PoolManager _poolManager;

    private float _maxTime;
    private float _intervalTime;
    private float _radius;
    private float _startTime;
    private float _currentDamage;
    private float _lastTime;

    private LayerMask _targetLayer;
    private bool _start;

    public void SetObjectData(float maxTime, float radius, float damage, float intervalTime, LayerMask targetLayer)
    {
        _maxTime = maxTime;
        _radius = radius;
        _currentDamage = damage;
        _intervalTime = intervalTime;
        _targetLayer = targetLayer;
    }

    public void StartPoison()
    {
        _start = true;
        _startTime = Time.time;
        _lastTime = 0f;
    }

    private void Update()
    {
        if (_start)
        {
            if (Time.time - _lastTime >= 0.5)
            {
                OnUpdatePoison();

                _lastTime = Time.time;

                if(Time.time - _startTime > _maxTime)
                {
                    _start = false;

                    this.gameObject.SetActive(false);

                    _poolManager.EnqueueObject(this.gameObject);
                }
            }
           
        }
    }

    private void OnUpdatePoison()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _targetLayer);

        foreach(var target in colliders)
        {
            IStatusEffect statusEffect = target.GetComponent<IStatusEffect>();

            if(statusEffect != null)
            {
                statusEffect.Poison(_currentDamage, _maxTime, _intervalTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

}
