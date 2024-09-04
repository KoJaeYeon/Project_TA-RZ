using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using BehaviorDesigner.Runtime;

public class EliteController : Monster_D
{
    public override void OnSetMonsterStat(float stat_Multiplier)
    {
        int rand = 4;
        _Ability = (MonsterAbility)rand;
        Anim = GetComponentInChildren<Animator>();

        StartCoroutine(LoadStat(stat_Multiplier));
        dashUi = GetComponentInChildren<Monster_D_DashUI>();
    }
}