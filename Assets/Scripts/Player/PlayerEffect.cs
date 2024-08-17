using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerEffect : MonoBehaviour
{
    [Inject] private PoolManager _poolManager;
    private Dictionary<AttackType, GameObject> _attackEffectDic = new Dictionary<AttackType, GameObject>();
    private Dictionary<AttackType, Transform> _attackEffectTransformDic = new Dictionary<AttackType, Transform>();

    [Header("Hit_Effect")]
    [SerializeField] private GameObject _hitEffect;
    [Header("Position")]
    [SerializeField] private GameObject _effectPosition;
    [Header("AttackEffect")]
    [SerializeField] private GameObject[] _attackEffectArray;

    private void Start()
    {
        InitializePlayerEffect();
    }

    private void InitializePlayerEffect()
    {
        var childTransformList = new List<Transform>();

        foreach(Transform childTransform in _effectPosition.transform)
        {
            childTransformList.Add(childTransform);
        }

        for(int i = 0; i < _attackEffectArray.Length; i++)
        {
            if(i < childTransformList.Count)
            {
                GameObject effect = Instantiate(_attackEffectArray[i], childTransformList[i]);
                _attackEffectDic.Add((AttackType)i, effect);
                _attackEffectTransformDic.Add((AttackType)i, childTransformList[i]);
            }
        }

        _poolManager.CreatePool(_hitEffect, 30);
    }

    public GameObject GetAttackEffect(AttackType attackType)
    {
        return _attackEffectDic[attackType];
    }

    public Transform GetAttackEffectTransform(AttackType attackType)
    {
        return _attackEffectTransformDic[attackType];   
    }

    public GameObject GetHitEffect()
    {
        GameObject hitEffect = _poolManager.DequeueObject(_hitEffect);

        return hitEffect;
    }

    public void ReturnHit(GameObject hitParticle)
    {
        ReturnHitParticle returnComponent = hitParticle.GetComponent<ReturnHitParticle>();

        StartCoroutine(returnComponent.ReturnParticle(hitParticle, _poolManager));
    }
}
