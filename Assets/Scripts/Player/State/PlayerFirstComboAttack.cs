using UnityEngine;

public class PlayerFirstComboAttack : PlayerState
{
    public PlayerFirstComboAttack(Player player) : base(player) { }
    
    private int _fristAttack = Animator.StringToHash("ComboAttack1");
    private int _comboFail = Animator.StringToHash("ComboFail");

    public override void StateEnter()
    {
        _player.IsNext = false;
        _animator.SetBool(_fristAttack, true);
    }

    public override void StateUpdate()
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if(animatorStateInfo.IsName("Attack1") && animatorStateInfo.normalizedTime >= 1f)
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
        _animator.SetBool(_fristAttack, false);
    }
}

public class PlayerSecondComboAttack : PlayerState
{
    public PlayerSecondComboAttack(Player player) : base(player) { }

    private int _secondAttack = Animator.StringToHash("ComboAttack2");
    private int _comboFail = Animator.StringToHash("ComboFail");

    public override void StateEnter()
    {
        _player.IsNext = false;
        _animator.SetBool(_secondAttack, true);
    }

    public override void StateUpdate()
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.IsName("Attack2") && animatorStateInfo.normalizedTime >= 1f)
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
        _animator.SetBool(_secondAttack, false);
    }
}

public class PlayerThirdComboAttack : PlayerState
{
    public PlayerThirdComboAttack(Player player) : base(player) { }
    
    private int _thirdAttack = Animator.StringToHash("ComboAttack3");
    private int _comboFail = Animator.StringToHash("ComboFail");

    public override void StateEnter()
    {
        _player.IsNext = false;
        _animator.SetBool(_thirdAttack, true);
    }

    public override void StateUpdate()
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if(animatorStateInfo.IsName("Attack3") && animatorStateInfo.normalizedTime >= 1f)
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
        _animator.SetBool(_thirdAttack, false);
    }
}

public class PlayerFourthComboAttack : PlayerState
{
    public PlayerFourthComboAttack(Player player) : base(player) { }

    private int _fourthAttack = Animator.StringToHash("ComboAttack4");

    public override void StateEnter()
    {
        _player.IsNext = false;
        _animator.SetBool(_fourthAttack, true);
    }

    public override void StateUpdate()
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.IsName("Attack3") && animatorStateInfo.normalizedTime >= 1f)
        {
            _state.ChangeState(State.Idle);
            return;
        }
    }

    public override void StateExit()
    {
        _animator.SetBool(_fourthAttack, false);
    }
}