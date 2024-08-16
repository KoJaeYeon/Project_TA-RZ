using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_RushTrail : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.DrawRushTrail();

        return TaskStatus.Success;
    }
}
