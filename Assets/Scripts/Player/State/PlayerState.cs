using UnityEngine;

public abstract class PlayerState : PlayerBaseState
{
    #region PlayerComponent
    protected Player _player;
    protected PlayerInputSystem _inputSystem;
    protected Rigidbody _rigidBody;
    protected Animator _animator;
    protected PlayerStateMachine _state;
    #endregion

    #region AnimationStringToHash
    protected readonly int _firstCombo = Animator.StringToHash("ComboAttack1");
    protected readonly int _secondCombo = Animator.StringToHash("ComboAttack2");
    protected readonly int _thirdCombo = Animator.StringToHash("ComboAttack3");
    protected readonly int _fourthCombo = Animator.StringToHash("ComboAttack4");
    protected readonly int _comboFail = Animator.StringToHash("ComboFail");
    protected readonly int _dashAnimation = Animator.StringToHash("Dash");
    protected readonly int _moveAnimation = Animator.StringToHash("Walk");
    #endregion

    public PlayerState(Player player)
    {
        InitializePlayerState(player);
    }

    private void InitializePlayerState(Player player)
    {
        _player = player;
        _inputSystem = player.GetComponent<PlayerInputSystem>();
        _rigidBody = player.GetComponent<Rigidbody>();
        _animator = player.GetComponentInChildren<Animator>();
        _state = player.GetComponent<PlayerStateMachine>();
    }

    protected void ChangeDash()
    {
        if (_inputSystem.IsDash)
        {
            _state.ChangeState(State.Dash);
        }
    }
}
