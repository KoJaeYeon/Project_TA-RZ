using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Gimmick")]
public class BTA_Boss_Gimmick : BossAction
{
    private readonly int _hashGimmick = Animator.StringToHash("Gimmick");

    public override void OnStart()
    {
        _anim.SetTrigger(_hashGimmick);
    }

    public override TaskStatus OnUpdate()
    {
        _owner.ActiveGimmick();

        return TaskStatus.Success;
    }
}
