using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using BehaviorDesigner.Runtime;

public class EliteController : Monster_D
{
    [Inject] UIEvent _uiEvent;
    public override void OnSetMonsterStat(float stat_Multiplier)
    {
        int rand = 4;
        _Ability = (MonsterAbility)rand;
        Anim = GetComponentInChildren<Animator>();

        StartCoroutine(LoadStat(stat_Multiplier));
        dashUi = GetComponentInChildren<Monster_D_DashUI>();
    }

    public override void Hit(float damage, float paralysisTime, Transform transform)
    {
        base.Hit(damage, paralysisTime, transform);
        if (Mon_Common_Hp_Remain <= 0)
        {
            _uiEvent.BlueChipUI.QuestCleared();
        }
    }
}