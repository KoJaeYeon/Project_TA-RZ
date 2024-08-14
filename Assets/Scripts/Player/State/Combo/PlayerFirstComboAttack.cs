using UnityEngine;

public class PlayerFirstComboAttack : PlayerComboAttack
{
    public PlayerFirstComboAttack(Player player) : base(player) { }

    public override void StateEnter()
    {
        _player.IsNext = false;

        _inputSystem.SetAttack(false);

        ComboAnimation(_firstCombo, true);
    }

    public override void StateUpdate()
    {
        base.StateUpdate(); 

        OnComboAttackUpdate("Attack1", State.SecondComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation(_firstCombo, false);

        base.StateExit();
    }
}
