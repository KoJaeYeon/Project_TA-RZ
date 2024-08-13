using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkillSystem : MonoBehaviour
{
    private enum SkillName
    {
        FristSkill = 49,
        SecondSkill = 74,
        ThirdSkill = 99,
        FourthSkill = 100
    }

    [Inject]
    private Player _player;
    private Dictionary<SkillName, Skill> _skillDictionary = new Dictionary<SkillName, Skill>();
    private float _maxGauge = 100f;
    private float _currentGauge = 56f;

    private void OnEnable()
    {
        InitializeSkill();
    }

    private void InitializeSkill()
    {
        _skillDictionary.Clear();

        _skillDictionary.Add(SkillName.FristSkill, new FirstSkill(_player, 20f, 25f));
        _skillDictionary.Add(SkillName.SecondSkill, new SecondSkill(_player, 20f, 50f));
        _skillDictionary.Add(SkillName.ThirdSkill, new ThirdSkill(_player, 20f, 75f));
        _skillDictionary.Add(SkillName.FourthSkill, new FourthSkill(_player, 20f, 100f));
    }

    public void SetSkillGauge(float value)
    {
        _currentGauge = Mathf.Clamp(_currentGauge + value, 0f, _maxGauge);
    }

    public Skill GetSkill()
    {
        if(_currentGauge <= 24f)
        {
            return null;
        }

        Skill playerSkill = null;
        
        foreach(var skill in _skillDictionary)
        {
            if(_currentGauge <= (float)skill.Key)
            {
                playerSkill = skill.Value;
                break;
            }
        }

        _currentGauge -= playerSkill.GetValue();

        return playerSkill;
    }

}
