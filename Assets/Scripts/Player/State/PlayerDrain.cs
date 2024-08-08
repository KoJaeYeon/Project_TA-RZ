using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrain : PlayerState
{
    private Animator _animator;
    private CharacterController _controller;
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;

    private readonly int _drain = Animator.StringToHash("Drain");

    private float _currentDrainRadius;
    private float _maxDrainRadius;
    private float _drainSpeed;

    public PlayerDrain(Player player) : base(player)
    {
        _animator = player.GetComponent<Animator>();
        _controller = player.GetComponent<CharacterController>();
        _inputSystem = player.GetComponent<PlayerInputSystem>();
        _state = player.GetComponent<PlayerStateMachine>();
    }

    public override void StateEnter()
    {
        _animator.SetBool(_drain,true);
        _currentDrainRadius = 0;
    }

    public override void StateUpdate()
    {
        if(_currentDrainRadius < _maxDrainRadius)
        {
            _currentDrainRadius += _drainSpeed * Time.deltaTime;
        }
    }

    public override void StateExit()
    {
        _animator.SetBool(_drain,false);
    }
}
