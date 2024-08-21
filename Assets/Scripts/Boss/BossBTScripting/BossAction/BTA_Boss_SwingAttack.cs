using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Swing")]
public class BTA_Boss_SwingAttack : BossAction
{
    private readonly int _hashSwing = Animator.StringToHash("Swing");

    public override void OnStart()
    {
        _anim.SetTrigger(_hashSwing);
        _owner.DrawSwing(false);
        _owner.SwingCool();
    }
}
