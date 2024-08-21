using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Smash")]
public class BTA_Boss_SmashAttack : BossAction
{
    private readonly int _hashSmash = Animator.StringToHash("Smash");

    public override void OnStart()
    {
        _anim.SetTrigger(_hashSmash);
        _owner.DrawSmash(false);
        _owner.SmashCool();
    }
}
