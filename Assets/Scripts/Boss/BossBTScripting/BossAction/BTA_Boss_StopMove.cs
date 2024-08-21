using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTA_Boss_StopMove : BossAction
{
    private NavMeshAgent _nav;

    private readonly int _hashMove = Animator.StringToHash("Move");

    public override void OnAwake()
    {
        base.OnAwake();
        _nav = GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        _anim.SetBool(_hashMove, false);
        _nav.isStopped = true;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
