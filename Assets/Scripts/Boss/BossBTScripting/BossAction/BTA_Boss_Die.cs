using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_Die : BossAction
{
    private readonly int _hashDie = Animator.StringToHash("Die");

    public override void OnStart()
    {
        _anim.SetTrigger(_hashDie);
        _owner.player.OnCalled_Achieve_BossKilled();
        if(_owner.isPlayerHit == false )
        {
            _owner.player.OnCalled_Achieve_NoHitBossKilled();
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
