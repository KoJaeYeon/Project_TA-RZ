using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum PoisonAttackType
{
    Sustained,
    Explosive
}

public class PoisonObject : MonoBehaviour
{
    [Inject]
    private PoolManager _poolManager;
    private event Action<float> _explosiveAction;
    private event Action<float, float, float> _sustainedAction;

    private float _maxTime;
    private float _intervalTime;
    private float _radius;
    private float _startTime;
    private float _currentDamage;
    private float _lastTime;

    private LayerMask _targetLayer;
    private bool _start;

    public void SetObjectData(float radius, float damage, LayerMask targetLayer, float maxTime = 0f, float intervalTime = 0f)
    {
        _maxTime = maxTime;
        _radius = radius;
        _currentDamage = damage;
        _intervalTime = intervalTime;
        _targetLayer = targetLayer;
    }

    public void StartPoison(PoisonAttackType attackType)
    {
        switch (attackType)
        {
            case PoisonAttackType.Sustained:
                Sustained();
                break;
            case PoisonAttackType.Explosive:
                Explosive();
                break;
        }
    }

    private void Explosive()
    {
        StartCoroutine(ExplosivePoison());
    }

    private void Sustained()
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

    private IEnumerator ExplosivePoison()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _targetLayer);

        foreach(var target in colliders)
        {
            IStatusEffect statusEffect = target.GetComponent<IStatusEffect>();

            if(statusEffect != null)
            {
                _explosiveAction += (float passivePower) => statusEffect.Explosion(passivePower);
                _sustainedAction += (float passivePower, float maxTime, float intervalTime) 
                    => statusEffect.Poison(passivePower, maxTime, intervalTime);
            }
        }

        yield return new WaitForSeconds(0.5f);

        _explosiveAction?.Invoke(_currentDamage);
        _sustainedAction?.Invoke(10f, 2f, 0.5f);

        _poolManager.EnqueueObject(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

}
