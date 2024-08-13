using UnityEngine;

public class PlayerSkill : PlayerState
{
    public PlayerSkill(Player player) : base(player)
    {
        _skillSystem = player.GetComponentInChildren<SkillSystem>();
    }

    #region SkillComponent
    private SkillSystem _skillSystem;
    private Skill _currentSkill;
    #endregion

    public override void StateEnter()
    {

    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void StateExit()
    {
        
    }

    private void InitializeSkill()
    {

    }

    public override void InputCheck()
    {
        
    }
}
