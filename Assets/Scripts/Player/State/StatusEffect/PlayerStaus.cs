using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaus : PlayerState
{
    public PlayerStaus(Player player) : base(player) { }

    protected Coroutine _coroutine;

    public override void StateExit()
    {
        _animator.speed = 1f;

        if (_coroutine != null)
        {
            _player.StopCoroutine(_coroutine);
        }
    }

    #region AnimatorStringToHash
    protected readonly int _comboFail = Animator.StringToHash("ComboFail");
    protected readonly int _paralysis = Animator.StringToHash("Paralysis");
    protected readonly int _speed = Animator.StringToHash("Speed");
    protected readonly int _knockBack = Animator.StringToHash("KnockBack");
    protected readonly int _deathCheck = Animator.StringToHash("DeathCheck");
    protected readonly int _death = Animator.StringToHash("Death");
    #endregion

    public override void InputCheck()
    {
        if (_inputSystem.IsSkill && _player.SkillCheck())
        {
            _state.ChangeState(State.Skill);
        }
    }
}
