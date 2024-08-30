using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Rush")]
public class BTA_Boss_ResetRushTime : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.ResetRushWaitTime();
        _owner.RushCool();

        return TaskStatus.Success;
    }
}
