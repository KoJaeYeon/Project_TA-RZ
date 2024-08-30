using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_RushMark
    : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.DrawRushMark();

        return TaskStatus.Success;
    }
}
