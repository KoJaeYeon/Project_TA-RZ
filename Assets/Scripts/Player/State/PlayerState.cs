using System;
using System.Collections.Generic;
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

    #region AnimatorStringToHash
    protected readonly int _dashAnimation = Animator.StringToHash("Dash");
    protected readonly int _dashChange = Animator.StringToHash("DashChange");
    protected readonly int _moveAnimation = Animator.StringToHash("Walk");
    protected readonly int _drain = Animator.StringToHash("Drain");
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

    protected virtual void ChangeStateBehaviour(PlayerInputSystem input)
    {
        if (input.Input != Vector2.zero)
        {
            _state.ChangeState(State.Run);
        }
        else if (input.IsDash)
        {
            _state.ChangeState(State.Dash);
        }
        else if (input.IsAttack)
        {
            _state.ChangeState(State.FirstComboAttack);
        }
        else if (input.IsDrain)
        {
            _state.ChangeState(State.Drain);
        }
        else if (input.IsSkill)
        {
            _state.ChangeState(State.Skill);
        }
    }
}
