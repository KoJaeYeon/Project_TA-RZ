using System.Net.Sockets;
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
}

public class PlayerSecondComboAttack : PlayerComboAttack
{
    public PlayerSecondComboAttack(Player player) : base(player) { }

    public override void StateEnter()
    {   
        _player.IsNext = false;
        ComboAnimation(_secondCombo, true);
    }

    public override void StateUpdate()
    {
        ChangeStateBehaviour(_inputSystem);
        OnComboAttackUpdate("Attack2", State.ThirdComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation (_secondCombo, false);
    }

}

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
}

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
}