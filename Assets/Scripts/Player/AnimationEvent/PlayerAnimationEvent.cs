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
       _effect.Active_FirstEffect();
        _attackDictionary[AttackType.firstAttack].Invoke();
    }

    //두 번째 공격 이펙트
    public void SecondAttack()
    {
        _effect.Active_SecondEffect();
        _attackDictionary[AttackType.secondAttack].Invoke();
    }

    //세 번째 공격 이펙트
    public void ThirdAttack()
    {
        _effect.Active_ThirdEffect();
        _attackDictionary[AttackType.thirdAttack].Invoke();
    }

    //네 번째 공격 이펙트
    public void FourthAttack()
    {
        _attackDictionary[AttackType.fourthAttack].Invoke();
    }

    public void SkillEnd()
    {
        _player.IsSkillAnimationEnd = true;
    }
}

