using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private PlayerEffect _effect;
    private Dictionary<AttackType, Action> _attackDictionary = new Dictionary<AttackType, Action>();

    private WaitForSeconds _returnTime = new WaitForSeconds(0.5f);

    private void Start()
    {
        _effect = gameObject.GetComponentInParent<PlayerEffect>();
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

    //다음 콤보 State로 넘겨주는 애니메이션 이벤트
    public void NextCombo()
    {
        _player.IsNext = true;
    }

    //첫 번째 공격 이펙트
    public void FirstAttack()
    {
        GameObject firstEffect = _effect.GetAttackEffect(AttackType.firstAttack);
        ParticleSystem effectParticle = firstEffect.GetComponent<ParticleSystem>();

        firstEffect.transform.localPosition = Vector3.zero;
        firstEffect.transform.localRotation = Quaternion.identity;
        firstEffect.transform.parent = null;
        effectParticle.Play();

        StartCoroutine(ReturnFirstEffect(firstEffect));

        _attackDictionary[AttackType.firstAttack].Invoke();
    }

    //두 번째 공격 이펙트
    public void SecondAttack()
    {
        GameObject secondEffect = _effect.GetAttackEffect(AttackType.secondAttack);
        ParticleSystem effectParticle = secondEffect.GetComponent<ParticleSystem>();

        secondEffect.transform.localPosition = Vector3.zero;
        secondEffect.transform.localRotation = Quaternion.identity;
        secondEffect.transform.parent = null;
        effectParticle.Play();

        StartCoroutine(ReturnSecondEffect(secondEffect));   

        _attackDictionary[AttackType.secondAttack].Invoke();
    }

    //세 번째 공격 이펙트
    public void ThirdAttack()
    {
        GameObject thirdEffect = _effect.GetAttackEffect(AttackType.thirdAttack);
        ParticleSystem effectParticle = thirdEffect.GetComponent<ParticleSystem>();

        thirdEffect.transform.localPosition = Vector3.zero;

        thirdEffect.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);



        thirdEffect.transform.parent = null;
        effectParticle.Play();

        StartCoroutine(ReturnThirdEffect(thirdEffect)); 

        _attackDictionary[AttackType.thirdAttack].Invoke();
    }

    //네 번째 공격 이펙트
    public void FourthAttack()
    {
        _attackDictionary[AttackType.fourthAttack].Invoke();
    }

    //종료
    private IEnumerator ReturnFirstEffect(GameObject effect)
    {
        yield return _returnTime;

        effect.transform.SetParent(_effect.GetAttackEffectTransform(AttackType.firstAttack));
    }

    private IEnumerator ReturnSecondEffect(GameObject effect)
    {
        yield return _returnTime;

        effect.transform.SetParent(_effect.GetAttackEffectTransform(AttackType.secondAttack));
    }

    private IEnumerator ReturnThirdEffect(GameObject effect)
    {
        yield return _returnTime;

        effect.transform.SetParent(_effect.GetAttackEffectTransform(AttackType.thirdAttack));
    }

    public void SkillEnd()
    {
        _player.IsSkillAnimationEnd = true;
    }
}

