using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondComboAttack : PlayerComboAttack
{
    public PlayerSecondComboAttack(Player player) : base(player) { }

    public override void StateEnter()
    {
       // _inputSystem.SetAttack(false);
        
        _player.IsNext = false;

        ComboAnimation(_secondCombo, true);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        OnComboAttackUpdate("Attack2", State.ThirdComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation(_secondCombo, false);

        base.StateExit();
    }
}
