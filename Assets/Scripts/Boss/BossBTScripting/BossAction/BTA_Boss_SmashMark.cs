using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[TaskCategory("Boss/Smash")]
public class BTA_Boss_SmashMark : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.DrawSmash(true);

        return TaskStatus.Success;
    }
}
