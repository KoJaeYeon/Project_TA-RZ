using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(Player player) : base(player) { }
    
    public override void StateEnter()
    {
        InitializeIdle();
    }

    public override void StateUpdate()
    {
        OnUpdateIdle();
    }

    public override void StateExit()
    {
        Exit();
    }

    private void InitializeIdle()
    {

    }

    private void OnUpdateIdle()
    {
        ChangeBehaviour();
    }

    private void ChangeBehaviour()
    {
        if (_inputSystem.Input != Vector2.zero)
        {
            _state.ChangeState(State.Run);
        }
        else if (_inputSystem.IsDash)
        {
            ChangeDash();
        }
        else if (_inputSystem.IsDrain == true)
        {
            _state.ChangeState(State.Drain);
        }
        else if (_inputSystem.IsAttack)
        {
            _state.ChangeState(State.FirstComboAttack);
        }
    }

    private void Exit()
    {
        _state.PreviousState = State.Idle;
    }
}
