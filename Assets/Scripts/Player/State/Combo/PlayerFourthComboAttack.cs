using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFourthComboAttack : PlayerComboAttack
{
    public PlayerFourthComboAttack(Player player) : base(player) { }
    
    //4초면 4초에 0.3f, 0.03f
    private float _chargeMaxTime = 2f;
    private float _currentChargeTime;
    
    public override void StateEnter()
    {
        _currentChargeTime = 0f;

        ComboAnimation(_fourthCombo, true);
    }

    public override void StateUpdate()
    {
        if (_inputSystem.IsAttack && _currentChargeTime <= _chargeMaxTime)
        {
            _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if(_animatorStateInfo.IsName("Attack_Legend_Anim") &&_animatorStateInfo.normalizedTime >= 0.3f)
            {
                _animator.speed = 0.05f;
            }
        }
        else
        {
            _animator.speed = 1f;

            _animator.SetBool(_fourthCombo, false);

            _state.ChangeState(State.Idle);

            return;
        }

        _currentChargeTime += Time.deltaTime;

        //if(_currentChargeTime >= _chargeMaxTime)
        //{
        //    ChargeAttack(_currentChargeTime);

        //    _animator.SetBool(_fourthCombo, false);

        //    _state.ChangeState(State.Idle);

        //    return;
        //}

        //if (!_inputSystem.IsAttack)
        //{
        //    ChargeAttack(_currentChargeTime);

        //    _animator.SetBool(_fourthCombo, false);

        //    _state.ChangeState(State.Idle);

        //    return;
        //}
    }

    public override void StateExit()
    {
        ComboAnimation(_fourthCombo, false);

        _inputSystem.SetAttack(false);

        base.StateExit();
    }

    private void ChargeAttack(float time)
    {
        if(time <= 1f)
        {
            FourthAttack(time);
        }

    }



    private void FourthAttack(float time)
    {

    }
}
