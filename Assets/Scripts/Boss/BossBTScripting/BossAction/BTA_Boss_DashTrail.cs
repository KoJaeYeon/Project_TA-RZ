using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_DashTrail : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.DrawDashTrail();

        return TaskStatus.Success;
    }
}
