using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Explosion")]
public class BTA_Boss_MarkFirstExplosion : BossAction
{
    private readonly int _hashExplosion = Animator.StringToHash("Explosion");

    public override void OnStart()
    {
        _anim.SetTrigger(_hashExplosion);
    }

    public override TaskStatus OnUpdate()
    {
        _owner.MarkFirstExplosion();

        return TaskStatus.Success;
    }
}
