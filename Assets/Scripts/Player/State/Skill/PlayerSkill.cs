using System;
using System.Collections;
using UnityEngine;

public class PlayerSkill : PlayerState
{
    public PlayerSkill(Player player) : base(player)
    {
        _skillSystem = _player.GetComponentInChildren<SkillSystem>();
        Debug.Log(_skillSystem);
    }

    #region AnimatorStringToHash
    private AnimatorStateInfo _animatorStateInfo;

    private readonly int _skill = Animator.StringToHash("Skill");
    private readonly int _skill_Index = Animator.StringToHash("Skill_Index");
    private readonly int _skill_3 = Animator.StringToHash("Skill_3");
    #endregion

    #region SkillComponent
    private PC_Skill _PC_Skill;
    private SkillSystem _skillSystem;
    #endregion

    #region skillValue
    private int skillIndex = 0;
    #endregion

    public override void StateEnter()
    {
       StartSkill();

       _animator.SetTrigger(_skill);

       _animator.SetInteger(_skill_Index, skillIndex);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        OnSkillUpdate();
    }

    public override void StateExit()
    {
        _player.IsSkillAnimationEnd = false;
        if (skillIndex == 3)
        {
            //액티브 스킬
            _player.IsSkillAcitve[skillIndex - 1] = false;
            _skillSystem.SetActive_Skiil_Effect(skillIndex, false);
        }
    }

    private void StartSkill()
    {
        _PC_Skill = GetSkill() as PC_Skill;
        _player.IsSkillAnimationEnd = false;
        SkillConsume();
        SelectSkillAndStart();
    }

    protected void OnSkillUpdate()
    {
        AnimatorStateInfo _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if(skillIndex == 3)
        {
            if (_animatorStateInfo.IsName("Skill_3_2") && _animatorStateInfo.normalizedTime >= 0.99f || _player.IsSkillAnimationEnd)
            {
                _state.ChangeState(State.Idle);
                return;
            }
        }
        else
        {
            if (_animatorStateInfo.IsName($"Skill_{skillIndex}") && _animatorStateInfo.normalizedTime >= 0.99f || _player.IsSkillAnimationEnd)
            {
                _state.ChangeState(State.Idle);
                return;
            }
        }

    }

    private void SelectSkillAndStart()
    {        
        switch(skillIndex)
        {
            case 1:
                _player.StartCoroutine(ApplySkillAndDuration());
                break;
            case 2:
                _player.StartCoroutine(ApplySkillAndDuration());
                break;
            case 3:
                _player.StartCoroutine(ApplySkillAndDuration());
                break;
            case 4:
                _player.StartCoroutine(ApplySkillAndDuration());
                break;
            default:
                Debug.LogError("Skill Error!");
                break;
        }
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

    IEnumerator ApplySkillAndDuration()
    {
        _player.IsSkillAcitve[skillIndex - 1] = true;
        _skillSystem.SetActive_Skiil_Effect(skillIndex, true);

        if (skillIndex == 1)
        {
            _player.gameObject.layer = LayerMask.NameToLayer("Ghost");
        }
        else if (skillIndex == 4)
        {
            _player.OnPropertyChanged(nameof(_player.CurrentAmmo));
        }

        if (skillIndex == 3)
        {
            yield return new WaitForSeconds(_PC_Skill.Skill_Value[0]);
            _animator.SetTrigger(_skill_3);
        }
        else
        {
            yield return new WaitForSeconds(_PC_Skill.Skill_Duration);

        }        

        if (skillIndex == 3)
        {
            _skillSystem.Active_Shield(_PC_Skill.Skill_Value[1]);
        }
        else
        {
            //지속시간 있는 스킬
            _player.IsSkillAcitve[skillIndex - 1] = false;
            _skillSystem.SetActive_Skiil_Effect(skillIndex, false);
        }

        if (skillIndex == 1)
        {
            _player.gameObject.layer = LayerMask.NameToLayer("Player");
        }
        else if (skillIndex == 4)
        {
            _player.OnPropertyChanged(nameof(_player.CurrentAmmo));
        }
    }
}
