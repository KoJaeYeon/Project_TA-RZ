using System.Collections;
using UnityEngine;

public class PlayerComboAttack : PlayerState
{
    public PlayerComboAttack(Player player) : base(player) { }

    protected PC_Attack _comboData;
    protected float _currentAtkMultiplier;
    protected int _currentGetSkillGauge;
    protected float _currentStiffT;

    public virtual IEnumerator LoadData(string idStr)
    {
        while (true)
        {
            var comboData = _player.dataManager.GetData(idStr) as PC_Attack;

            if(comboData == null)
            {
                Debug.Log("콤보 데이터를 가져오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                _comboData = comboData;
                Debug.Log("콤보 데이터를 성공적으로 가져왔습니다.");
                yield break;
            }
        }
    }

    protected virtual void ChangeData(int currentLevel)
    {
        _currentAtkMultiplier = _comboData.Atk_Multiplier;
        _currentStiffT = _comboData.AbnStatus_Value;

        if (currentLevel == 4)
        {
            _currentGetSkillGauge = 0;
            return;
        }

        _currentGetSkillGauge = _comboData.Arm_SkillGageGet[currentLevel];

    }

    #region AnimatorStringToHash
    protected AnimatorStateInfo _animatorStateInfo;

    protected readonly int _firstCombo = Animator.StringToHash("ComboAttack1");
    protected readonly int _secondCombo = Animator.StringToHash("ComboAttack2");
    protected readonly int _thirdCombo = Animator.StringToHash("ComboAttack3");
    protected readonly int _fourthCombo = Animator.StringToHash("ComboAttack4");
    protected readonly int _comboFail = Animator.StringToHash("ComboFail");
    #endregion

    protected void OnComboAttackUpdate(string attackName, State nextCombo)
    {
        
            
        _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (_animatorStateInfo.IsName(attackName) && _animatorStateInfo.normalizedTime >= 0.99f)
        {
            _animator.SetTrigger(_comboFail);
            _state.ChangeState(State.Idle);
            return;
        }
        else
        {
            AttackRotation();
        }


        if (nextCombo is State.FourthComboAttack)
        {
            if(_player.CurrentAmmo <= 1 && _player.CurrentLevel != 4)
            {
                return;
            }
        }

        if (_inputSystem.IsAttack && _player.IsNext)
        {
            _state.ChangeState(nextCombo);
        }
    }

    public override void StateEnter()
    {
        _inputSystem.SetAttack(false);
    }

    protected void ComboAnimation(int hashValue, bool isPlay)
    {
        _animator.SetBool(hashValue, isPlay);
    }

    protected void AttackRotation()
    {
        float targetRotation = _player.MainCamera.transform.rotation.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);

        _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation, rotation, 10f * Time.deltaTime);
    }

    public override void InputCheck()
    {
        if (_inputSystem.IsDash && _player.StaminaCheck())
        {
            _state.ChangeState(State.Dash);
        }
        else if (_inputSystem.IsSkill && _player.SkillCheck())
        {
            _state.ChangeState(State.Skill);
        }
    }

    public override void StateExit()
    {        
        _player.CurrentAmmo -= _player.IsSkillAcitve[1] ? 0 : _player._PC_Level.Level_Consumption;
    }
}
