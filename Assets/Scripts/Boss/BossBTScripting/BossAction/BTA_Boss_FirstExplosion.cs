using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Explosion")]
public class BTA_Boss_FirstExplosion : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.FirstExplosion();

        return TaskStatus.Success;
    }
}