using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Gimmick")]
public class BTA_Boss_MarkGimmick : BossAction
{
    private readonly int _hashExplosion = Animator.StringToHash("Explosion");
    private readonly int _hashGimmickReady = Animator.StringToHash("GimmickReady");

    public override void OnStart()
    {
        _anim.ResetTrigger(_hashExplosion);
        _anim.SetTrigger(_hashGimmickReady);
    }

    public override TaskStatus OnUpdate()
    {
        _owner.MarkGimmick();

        return TaskStatus.Success;
    }
}
