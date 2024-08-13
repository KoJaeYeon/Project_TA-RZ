using UnityEngine;

public class PlayerFirstComboAttack : PlayerComboAttack
{
    public PlayerFirstComboAttack(Player player) : base(player) { }

    public override void StateEnter()
    {
        _player.IsNext = false;

        ComboAnimation(_firstCombo, true);
    }

    public override void StateUpdate()
    {
        ChangeStateBehaviour(_inputSystem);

        OnComboAttackUpdate("Attack1", State.SecondComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation(_firstCombo, false);
    }

    private void FirstAttack()
    {
        //1타 공격 로직
    }
}
