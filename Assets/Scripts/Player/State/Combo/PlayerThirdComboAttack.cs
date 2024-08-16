using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirdComboAttack : PlayerComboAttack
{
    public PlayerThirdComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.thirdAttack, ThirdAttack);
    }

    public override void StateEnter()
    {
        _player.IsNext = false;

        ComboAnimation(_thirdCombo, true);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        OnComboAttackUpdate("Attack3", State.FourthComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation(_thirdCombo, false);

        base.StateExit();
    }

    private void ThirdAttack()
    {

    }

}
