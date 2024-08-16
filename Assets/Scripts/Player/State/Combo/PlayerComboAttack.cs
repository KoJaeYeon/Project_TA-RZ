using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerComboAttack : PlayerState
{
    public PlayerComboAttack(Player player) : base(player) { }

    public override void StateExit()
    {
        _player.CurrentAmmo -= _player.IsSkillAcitve[1] ? 0 : _player._PC_Level.Level_Consumption;
    }

    #region AnimatorStringToHash
    protected AnimatorStateInfo _animatorStateInfo;

    protected readonly int _firstCombo = Animator.StringToHash("ComboAttack1");
    protected readonly int _secondCombo = Animator.StringToHash("ComboAttack2");
    protected readonly int _thirdCombo = Animator.StringToHash("ComboAttack3");
    protected readonly int _fourthCombo = Animator.StringToHash("ComboAttack4");
    protected readonly int _comboFail = Animator.StringToHash("ComboFail");
    #endregion

    protected void OnComboAttackUpdate(string attackName, State nextCombo)
    {
        _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (_animatorStateInfo.IsName(attackName) && _animatorStateInfo.normalizedTime >= 0.99f)
        {
            _animator.SetTrigger(_comboFail);
            _state.ChangeState(State.Idle);
            return;
        }
        else
            AttackRotation();
        

        if (_inputSystem.IsAttack && _player.IsNext)
        {
            _state.ChangeState(nextCombo);
        }
    }

    protected void ComboAnimation(int hashValue, bool isPlay)
    {
        _animator.SetBool(hashValue, isPlay);
    }

    protected void AttackRotation()
    {
        float targetRotation = _player.MainCamera.transform.rotation.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);

        _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation, rotation, 10f * Time.deltaTime);
    }

    public override void InputCheck()
    {
        if (_inputSystem.IsDash && _player.StaminaCheck())
        {
            _state.ChangeState(State.Dash);
        }
        else if (_inputSystem.IsSkill)
        {
            _state.ChangeState(State.Skill);
        }
    }
}
