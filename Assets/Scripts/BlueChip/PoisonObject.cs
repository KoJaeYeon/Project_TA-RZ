using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonObject : MonoBehaviour
{
    private event Action<float, float, float> _poisonAction;

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

        _start = true;
        _startTime = Time.time;
        _lastTime = 0f;
    }

    private void Update()
    {
        if (_start)
        {
            if(Time.time - _startTime > _maxTime)
            {
                _start = false;

                this.gameObject.SetActive(false);

                return;
            }

            if(Time.time - _lastTime >= 0.2)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _targetLayer);

                foreach (var target in colliders)
                {
                    IStatusEffect statusEffect = target.GetComponent<IStatusEffect>();

                    if (statusEffect != null)
                    {
                        _poisonAction += (float currentDamage, float maxTime, float intervalTime) => statusEffect.Poison(currentDamage, maxTime, intervalTime);
                    }
                }

                _poisonAction?.Invoke(_currentDamage, _maxTime, _intervalTime);

                _poisonAction = null;

                _lastTime = Time.time;
            }   
        }
    }

}
