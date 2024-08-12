using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_DashAttack : BossAction
{
    [SerializeField] private SharedFloat _dashSpeed;
    [SerializeField] private SharedFloat _dashRange;

    private Vector3 _startPos;

    private bool _isDash = false;

    private readonly int _hashDash = Animator.StringToHash("DashAttack");

    public override void OnStart()
    {
        _anim.SetTrigger(_hashDash);

        _startPos = transform.position;
        _isDash = true;

        _owner.DashAttack(_dashSpeed.Value);
    }

    public override TaskStatus OnUpdate()
    {
        if (_isDash)
        { 
            float distance = Vector3.Distance(transform.position, _startPos);

            if (distance >= _dashRange.Value)
            {
                _rb.velocity = Vector3.zero;
                _isDash = false;
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Running;
    }
}
