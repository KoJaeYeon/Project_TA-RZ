using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Rush")]
public class BTC_Boss_RushCoolDown : BossConditional
{
    public override TaskStatus OnUpdate()
    {
        if(_boss.Value.isCoolSmash && _boss.Value.isCoolSwing) return TaskStatus.Success;

        return TaskStatus.Failure;
    }
}
