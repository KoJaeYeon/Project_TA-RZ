using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFourthComboAttack : PlayerComboAttack
{
    public PlayerFourthComboAttack(Player player) : base(player) { }

    public override void StateEnter()
    {
        _player.IsNext = false;

        ComboAnimation(_fourthCombo, true);
    }

    public override void StateUpdate()
    {
        ChangeStateBehaviour(_inputSystem);

        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.IsName("Attack4") && animatorStateInfo.normalizedTime >= 0.95f)
        {
            _state.ChangeState(State.Idle);
            return;
        }
        else
            AttackRotation();
    }

    public override void StateExit()
    {
        ComboAnimation(_fourthCombo, false);
    }

    private void FourthAttack()
    {
        //4타 공격 로직
    }

}
