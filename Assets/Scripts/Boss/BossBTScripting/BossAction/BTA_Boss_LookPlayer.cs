using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_LookPlayer : BossAction
{
    public override TaskStatus OnUpdate()
    {
        _owner.LookAtPlayer();

        float angle = Quaternion.Angle(transform.rotation, _owner.PlayerRot());
       
        if (angle < 0.1f)
        { 
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
