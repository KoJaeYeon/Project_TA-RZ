using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(Player player) : base(player) { }

    #region State
    public override void StateUpdate()
    {
        base.StateUpdate();
    }
    #endregion 

    public override void InputCheck()
    {
        if (_inputSystem.Input != Vector2.zero)
        {
            _state.ChangeState(State.Run);
        }
        else if (_inputSystem.IsDash && _player.StaminaCheck())
        {
            _state.ChangeState(State.Dash);
        }
        else if (_inputSystem.IsAttack)
        {
            _state.ChangeState(State.FirstComboAttack);
        }
        else if (_inputSystem.IsDrain && _player.StaminaCheck())
        {
            _state.ChangeState(State.Drain);
        }
        else if (_inputSystem.IsSkill && _player.SkillCheck())
        {
            _state.ChangeState(State.Skill);
        }
    }
}
