using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/RootSmash")]
public class BTA_Boss_MarkRootSmash : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.MarkRootSmash();

        return TaskStatus.Success;
    }
}
