using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    protected float _power;
    protected float _useValue;

    public Skill(float power, float useValue)
    {//각 스킬의 기본 정보를 정의.
        _power = power;
        _useValue=useValue;
    }

    public abstract void UseSkill();
    public abstract void SetSkillPower(float power);
}

public class FirstSkill : Skill
{
    public FirstSkill(float power, float useValue) : base(power, useValue) { }


    public override void SetSkillPower(float power)
    {
        _power += power;
    }

    public override void UseSkill()
    {
        Debug.Log("1번 스킬");
    }
}

public class SecondSkill : Skill
{
    public SecondSkill(float power, float useValue) : base(power, useValue) { }

    public override void SetSkillPower(float power)
    {
        _power += power;
    }

    public override void UseSkill()
    {
        Debug.Log("2번 스킬");
    }
}

public class ThirdSkill : Skill
{
    public ThirdSkill(float power, float useValue) : base(power, useValue) { }

    public override void SetSkillPower(float power)
    {
        _power += power;
    }

    public override void UseSkill()
    {
        Debug.Log("3번 스킬");
    }
}

public class FourthSkill : Skill
{
    public FourthSkill(float power, float useValue) : base(power, useValue) { }

    public override void SetSkillPower(float power)
    {
        _power += power;
    }

    public override void UseSkill()
    {
        Debug.Log("4번 스킬");
    }
}
