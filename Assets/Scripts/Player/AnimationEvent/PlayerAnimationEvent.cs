using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

public enum AttackType
{
    firstAttack,
    secondAttack,
    thirdAttack,
    fourthAttack
}

public class PlayerAnimationEvent : MonoBehaviour
{
    [Inject]
    private Player _player;
    private Dictionary<AttackType, Action> _attackDictionary = new Dictionary<AttackType, Action>();
    private Dictionary<AttackType, GameObject> _effectDictionary = new Dictionary<AttackType, GameObject>();
    private Dictionary<AttackType, Transform> _effectTransform = new Dictionary<AttackType, Transform>();

    [Header("AttackEffect")]
    [SerializeField] private GameObject[] _effects;
    [Header("EffectPosition")]
    [SerializeField] private GameObject _effectTransformobject;

    private void Start()
    {
        InitializeEffect();
    }

    private void InitializeEffect()
    {
        var childEffectList = new List<Transform>();   

        foreach(Transform child in _effectTransformobject.transform)
        {
            childEffectList.Add(child);
        }

        for(int i = 0; i < _effects.Length; i++)
        {
            if(i < childEffectList.Count)
            {
                GameObject effect = Instantiate(_effects[i], childEffectList[i]);
                _effectDictionary.Add((AttackType)i, effect);
                _effectTransform.Add((AttackType)i, childEffectList[i]);
            }
        }
    }

    public void AddEvent(AttackType attacktype, Action callBack)
    {
        if (!_attackDictionary.ContainsKey(attacktype))
        {
            _attackDictionary.Add(attacktype, callBack);
        }
        else
            return;
    }

    private GameObject GetEffect(AttackType attacktype)
    {
        return _effectDictionary[attacktype];
    }

    private Transform GetEffectTransform(AttackType attacktype)
    {
        return _effectTransform[attacktype];
    }

    //다음 콤보 State로 넘겨주는 애니메이션 이벤트
    public void NextCombo()
    {
        _player.IsNext = true;
    }

    public void FirstAttack()
    {
        GameObject firstEffect = GetEffect(AttackType.firstAttack);
        ParticleSystem effectParticle = firstEffect.GetComponent<ParticleSystem>();

        firstEffect.transform.localPosition = Vector3.zero;
        firstEffect.transform.localRotation = Quaternion.identity;
        firstEffect.transform.parent = null;
        effectParticle.Play();

        StartCoroutine(ReturnFirstEffect(firstEffect));

        _attackDictionary[AttackType.firstAttack].Invoke();
    }

    public void SecondAttack()
    {
        GameObject secondEffect = GetEffect(AttackType.secondAttack);
        ParticleSystem effectParticle = secondEffect.GetComponent<ParticleSystem>();

        secondEffect.transform.localPosition = Vector3.zero;
        secondEffect.transform.localRotation = Quaternion.identity;
        secondEffect.transform.parent = null;
        effectParticle.Play();

        StartCoroutine(ReturnSecondEffect(secondEffect));   

        _attackDictionary[AttackType.secondAttack].Invoke();
    }

    public void ThirdAttack()
    {
        _attackDictionary[AttackType.thirdAttack].Invoke();
    }

    public void FourthAttack()
    {
        _attackDictionary[AttackType.fourthAttack].Invoke();
    }

    private IEnumerator ReturnFirstEffect(GameObject effect)
    {
        yield return new WaitWhile(() => effect.GetComponent<ParticleSystem>().isPlaying);

        effect.transform.SetParent(GetEffectTransform(AttackType.firstAttack));
    }

    private IEnumerator ReturnSecondEffect(GameObject effect)
    {
        yield return new WaitWhile(()=>effect.GetComponent<ParticleSystem>().isPlaying);

        effect.transform.SetParent(GetEffectTransform(AttackType.secondAttack));
    }
}

