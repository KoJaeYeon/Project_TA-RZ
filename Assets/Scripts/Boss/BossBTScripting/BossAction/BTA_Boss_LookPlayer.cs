using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_LookPlayer : BossAction
{
    private Quaternion _rot;

    public override void OnStart()
    {
        _rot = _owner.PlayerRot();
    }

    public override TaskStatus OnUpdate()
    {
        _owner.LookAtPlayer(_rot);

        float angle = Quaternion.Angle(transform.rotation, _rot);
       
        if (angle < 0.1f)
        { 
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
