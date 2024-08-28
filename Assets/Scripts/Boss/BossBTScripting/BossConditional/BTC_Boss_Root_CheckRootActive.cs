using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Root")]
public class BTC_Boss_Root_CheckRootActive : BossConditional
{
    public override TaskStatus OnUpdate()
    {
        if (_owner == null)
        {
            Debug.LogWarning("보스가 없음");
            return TaskStatus.Failure;
        }

        if (_owner.CheckRootActive()) return TaskStatus.Success;

        return TaskStatus.Failure;
    }
}
