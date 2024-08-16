using UnityEngine;

public class PlayerSkill : PlayerState
{
    public PlayerSkill(Player player) : base(player)
    {
    }

    #region SkillComponent
    private PC_Skill _PC_Skill;
    #endregion

    #region skillValue
    private int skillIndex = 0;
    #endregion

    public override void StateEnter()
    {
       StartSkill();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log(_PC_Skill);
    }

    public override void StateExit()
    {
        
    }

    private void StartSkill()
    {
        _PC_Skill = GetSkill() as PC_Skill;
        SkillConsume();
    }

    private Data GetSkill()
    {
        if(_player.CurrentSkill < _player._skillCounption[1])
        {
            skillIndex = 1;
            return _player.dataManager.GetData("S201");
        }
        else if (_player.CurrentSkill < _player._skillCounption[2])
        {
            skillIndex = 2;
            return _player.dataManager.GetData("S202");
        }
        else if (_player.CurrentSkill < _player._skillCounption[3])
        {
            skillIndex = 3;
            return _player.dataManager.GetData("S203");
        }
        else
        {
            skillIndex = 4;
            return _player.dataManager.GetData("S204");

        }
    }

    private void SkillConsume()
    {
        _player.CurrentSkill -= _PC_Skill.Skill_Gauge_Consumption;
    }

    public override void InputCheck()
    {
        if (_inputSystem.IsDash && _player.StaminaCheck())
        {
            _state.ChangeState(State.Dash);
        }
    }
}
