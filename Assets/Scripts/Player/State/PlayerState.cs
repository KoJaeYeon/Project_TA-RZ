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
