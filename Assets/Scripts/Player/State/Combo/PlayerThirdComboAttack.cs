using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirdComboAttack : PlayerComboAttack
{
    public PlayerThirdComboAttack(Player player) : base(player) { }

    public override void StateEnter()
    {
        _player.IsNext = false;

        ComboAnimation(_thirdCombo, true);
    }

    public override void StateUpdate()
    {
        ChangeStateBehaviour(_inputSystem);

        OnComboAttackUpdate("Attack3", State.FourthComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation(_thirdCombo, false);
    }

    private void ThirdAttack()
    {
        //3타 공격 로직
    }
}
