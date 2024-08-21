using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[TaskCategory("Boss/RootSmash")]
public class BTA_Boss_RootSmash : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.RootSmash();

        return TaskStatus.Success;
    }
}
