using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(Player player) : base(player) { }

    #region State
    public override void StateEnter()
    {
        InitializeIdle();
    }

    public override void StateUpdate()
    {
        OnUpdateIdle();
    }
    #endregion 

    private void InitializeIdle()
    {
        
    }

    private void OnUpdateIdle()
    {
        ChangeStateBehaviour(_inputSystem);
    }

    protected override void ChangeStateBehaviour(PlayerInputSystem input)
    {
        base.ChangeStateBehaviour(input);
    }
}
