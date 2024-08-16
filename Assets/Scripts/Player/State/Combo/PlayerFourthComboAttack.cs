using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFourthComboAttack : PlayerComboAttack
{
    public PlayerFourthComboAttack(Player player) : base(player)
    {
        //PlayerAnimationEvent _event;
        //_event = player.GetComponentInChildren<PlayerAnimationEvent>();
        //_event.AddEvent(AttackType.fourthAttack, FourthAttack);
    }

    private float _chargeMaxTime = 4f;
    private float _currentChargeTime;

    public override void StateEnter()
    {
        ComboAnimation(_fourthCombo, true);
    }

    public override void StateUpdate()
    {
        ChargeAttack(_currentChargeTime);

        if (!_inputSystem.IsAttack)
        {
            _animator.SetBool(_fourthCombo, false);

            _state.ChangeState(State.Idle);

            return;
        }

        _currentChargeTime += Time.deltaTime;
    }

    public override void StateExit()
    {
        ComboAnimation(_fourthCombo, false);

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
