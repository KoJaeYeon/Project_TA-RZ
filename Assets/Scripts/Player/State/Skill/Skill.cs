using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    #region SkillComponent
    protected Player _player;
    protected Animator _animator;
    protected Rigidbody _rigidbody;
    protected PlayerStateMachine _state;
    protected PlayerInputSystem _inputSystem;
    #endregion

    #region Value
    protected float _power;
    protected float _useValue;
    #endregion

    public Skill(Player player,float power, float useValue)
    {//각 스킬의 기본 정보를 정의.
        _player = player;
        _power = power;
        _useValue = useValue;

        InitializeSkill(player);
    }

    private void InitializeSkill(Player player)
    {
        _animator = player.GetComponent<Animator>();
        _rigidbody = player.GetComponent<Rigidbody>();  
        _inputSystem = player.GetComponent<PlayerInputSystem>();
        _state = player.GetComponent<PlayerStateMachine>(); 
    }

    #region Abstract
    public abstract void SetSkillPower(float power);
    public abstract float GetValue();
    #endregion

    #region Virtual
    public virtual void OnStartSkill() { }
    public virtual void OnUpdateSkill() { }
    public virtual void OnExitSkill() { }
    #endregion
}

public class FirstSkill : Skill
{
    public FirstSkill(Player player, float power, float useValue) : base(player, power, useValue) { }

    public override float GetValue()
    {
        return _useValue;
    }

    public override void SetSkillPower(float power)
    {
        _power += power;
    }




    
}

public class SecondSkill : Skill
{
    public SecondSkill(Player player, float power, float useValue) : base(player, power, useValue) { }

    public override float GetValue()
    {
        return _useValue;
    }

    public override void SetSkillPower(float power)
    {
        _power += power;
    }

    
}

public class ThirdSkill : Skill
{
    public ThirdSkill(Player player, float power, float useValue) : base(player, power, useValue) { }

    public override float GetValue()
    {
        return _useValue;
    }

    public override void SetSkillPower(float power)
    {
        _power += power;
    }

    
}

public class FourthSkill : Skill
{
    public FourthSkill(Player player, float power, float useValue) : base(player, power, useValue) { }

    public override float GetValue()
    {
        return _useValue;
    }

    public override void SetSkillPower(float power)
    {
        _power += power;
    }

    
}
