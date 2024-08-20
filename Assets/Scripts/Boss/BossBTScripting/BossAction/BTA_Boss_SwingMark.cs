using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Swing")]
public class BTA_Boss_SwingMark : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.DrawSwing(true);

        return TaskStatus.Success;
    }
}
