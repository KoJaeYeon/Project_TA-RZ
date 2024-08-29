using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTC_Boss_StandartPhaseHp : BossConditional
{
    [SerializeField] private SharedFloat _standardHp;
    [SerializeField] private BossPhase _phase;

    public override TaskStatus OnUpdate()
    {
        if (_owner == null)
        {
            Debug.LogWarning("보스가 없음");
            return TaskStatus.Failure;
        }

        if (_owner.CheckPhase(_standardHp.Value)) return TaskStatus.Success;

        _boss.Value.phase = _phase;

        if (_boss.Value.phase == BossPhase.Phase2)
        {
            _owner.tag = "Monster";
        }

        return TaskStatus.Failure;
    }
}
