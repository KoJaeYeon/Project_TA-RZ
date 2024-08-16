using System;
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
    private Dictionary<AttackType, Action> _attackDictionary = new Dictionary<AttackType, Action>();

    [Header("AttackEffect")]
    [SerializeField] private GameObject _firstEffect;
    [SerializeField] private GameObject _secondEffect;
    [SerializeField] private GameObject _thirdEffect;
    [SerializeField] private GameObject _fourthEffect;

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

    public void FirstAttack()
    {
        _attackDictionary[AttackType.firstAttack].Invoke();
    }

    public void SecondAttack()
    {
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
}

