using UnityEngine;

public class PlayerIdle : PlayerState
{
    private Animator _animator;
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;

    public PlayerIdle(Player player) : base(player)
    {
        _animator = player.GetComponent<Animator>();
        _inputSystem = player.GetComponent<PlayerInputSystem>();    
        _state = player.GetComponent<PlayerStateMachine>(); 
    }

    public override void StateEnter()
    {
        
    }

    public override void StateUpdate()
    {
        if(_inputSystem.Input != Vector2.zero)
        {
            _state.ChangeState(State.Run);
        }
        else if(_inputSystem.IsDrain == true)
        {
            _state.ChangeState(State.Drain);
        }
    }

    public override void StateExit()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }
}
