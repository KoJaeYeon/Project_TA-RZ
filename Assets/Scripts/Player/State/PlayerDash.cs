using UnityEditor.XR.Oculus;
using UnityEngine;

public class PlayerDash : PlayerState
{
    public PlayerDash(Player player) : base(player)
    {
        _rigidBody = player.GetComponent<Rigidbody>();
        _state = player.GetComponent<PlayerStateMachine>();
        _animator = player.GetComponentInChildren<Animator>();
    }

    private Rigidbody _rigidBody;
    private PlayerStateMachine _state;
    private Animator _animator;

    private float _maxDistance;
    private float _dashSpeed;
    private float _dashDistance;

    public override void StateEnter()
    {
        InitializeDash();
    }

    public override void StateExit()
    {
        Exit();
    }


    private void InitializeDash()
    {

    }

    private void Exit()
    {

    }



}
