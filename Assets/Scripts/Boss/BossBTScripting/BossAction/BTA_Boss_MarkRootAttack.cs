using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/RootAttack")]
public class BTA_Boss_MarkRootAttack : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.MarkActiveRoot();

        return TaskStatus.Success;
    }
}
