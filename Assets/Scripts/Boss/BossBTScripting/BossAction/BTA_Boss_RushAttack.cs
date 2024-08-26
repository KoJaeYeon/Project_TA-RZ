using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Boss_RushAttack : BossAction
{
    [SerializeField] private SharedFloat _dashSpeed;
    [SerializeField] private SharedFloat _dashRange;

    private Vector3 _startPos;
    Vector3 direction;

    private bool _isDash = false;

    private readonly int _hashRush = Animator.StringToHash("Rush");

    public override void OnStart()
    {
        _anim.SetBool(_hashRush, true);

        _startPos = transform.position;
        _isDash = true;

        direction = _owner.SetRushDirection();

        _rb.isKinematic = false;
    }

    public override TaskStatus OnUpdate()
    {
        //_owner.RushAttack(_dashSpeed.Value, direction);
        _rb.velocity = direction * _dashSpeed.Value;

        if (_isDash)
        { 
            float distance = Vector3.Distance(transform.position, _startPos);

            if (distance >= _dashRange.Value)
            {
                _isDash = false;
            }
        }

        if (!_isDash)
        {
            _rb.velocity = Vector3.zero;
            _anim.SetBool(_hashRush, false);
            _rb.isKinematic = true;
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
            {
                _isDash = false;
            }
        }
    }
}
