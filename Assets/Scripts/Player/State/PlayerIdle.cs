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
        if (_inputSystem.Input != Vector2.zero)
        {
            _state.ChangeState(State.Run);
        }

        if (_inputSystem.IsDash)
        {
            ChangeDash();
        }
    }

    private void Exit()
    {
        _state.PreviousState = State.Idle;
    }
}
