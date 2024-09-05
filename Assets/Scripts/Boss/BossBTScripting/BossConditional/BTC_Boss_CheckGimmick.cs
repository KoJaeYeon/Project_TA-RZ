using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTC_Boss_CheckGimmick : BossConditional
{
    public override TaskStatus OnUpdate()
    {
        if (_owner.isGimmick) return TaskStatus.Failure;
        
        return TaskStatus.Success;
    }
}
