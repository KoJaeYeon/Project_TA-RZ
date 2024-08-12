using UnityEngine;

public class PlayerFirstComboAttack : PlayerState
{
    public PlayerFirstComboAttack(Player player) : base(player) { }
   
    public override void StateEnter()
    {
        _player.IsNext = false;
        _animator.SetBool(_firstCombo, true);
    }

    public override void StateUpdate()
    {

        if (_inputSystem.IsDash)
        {
            _animator.SetTrigger(_comboFail);
            _state.ChangeState(State.Dash);
        }

        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.normalizedTime < 0.99f)
        {
            AttackRotation();
        }

        if (animatorStateInfo.IsName("Attack1") && animatorStateInfo.normalizedTime >= 0.99f)
        {
            _animator.SetTrigger(_comboFail);
            _state.ChangeState(State.Idle);
            return;
        }

        if(_inputSystem.IsAttack && _player.IsNext)
        {
            _state.ChangeState(State.SecondComboAttack);
        }
        
    }

    public override void StateExit()
    {
        _animator.SetBool(_firstCombo, false);
    }
}

public class PlayerSecondComboAttack : PlayerState
{
    public PlayerSecondComboAttack(Player player) : base(player) { }

    public override void StateEnter()
    {   
        _player.IsNext = false;

        _animator.SetBool(_secondCombo, true);
    }

    public override void StateUpdate()
    {
        
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.normalizedTime < 0.99f)
        {
            AttackRotation();
        }

        if (animatorStateInfo.IsName("Attack2") && animatorStateInfo.normalizedTime >= 0.99f)
        {
            _animator.SetTrigger(_comboFail);
            _state.ChangeState(State.Idle);
            return;
        }

        if (_inputSystem.IsAttack && _player.IsNext)
        {
            _state.ChangeState(State.ThirdComboAttack);
        }

    }

    public override void StateExit()
    {
        _animator.SetBool(_secondCombo, false);
    }

}

public class PlayerThirdComboAttack : PlayerState
{
    public PlayerThirdComboAttack(Player player) : base(player) { }
   
    public override void StateEnter()
    {
        _player.IsNext = false;
        _animator.SetBool(_thirdCombo, true);
    }

    public override void StateUpdate()
    {

        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.normalizedTime < 0.99f)
        {
            AttackRotation();
        }

        if (animatorStateInfo.IsName("Attack3") && animatorStateInfo.normalizedTime >= 0.99f)
        {
            _animator.SetTrigger(_comboFail);
            _state.ChangeState(State.Idle);
            return;
        }

        if(_inputSystem.IsAttack && _player.IsNext)
        {
            _state.ChangeState(State.FourthComboAttack);
        }
    }

    public override void StateExit()
    {
        _animator.SetBool(_thirdCombo, false);
    }
}

public class PlayerFourthComboAttack : PlayerState
{
    public PlayerFourthComboAttack(Player player) : base(player) { }

    public override void StateEnter()
    {
        _player.IsNext = false;
        _animator.SetBool(_fourthCombo, true);
    }

    public override void StateUpdate()
    {
       
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if(animatorStateInfo.normalizedTime < 0.99f)
        {
            AttackRotation();
        }

        if (animatorStateInfo.IsName("Attack4") && animatorStateInfo.normalizedTime >= 0.99f)
        {
            _state.ChangeState(State.Idle);
            return;
        }
    }

    public override void StateExit()
    {
        _animator.SetBool(_fourthCombo, false);
    }
}