using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BTA_Boss_RushAttack : BossAction
{
    [SerializeField] private SharedFloat _dashSpeed;
    [SerializeField] private SharedFloat _dashRange;

    private Vector3 _startPos;
    Vector3 direction;

    private bool _isRush = false;

    private readonly int _hashRush = Animator.StringToHash("Rush");

    public override void OnStart()
    {
        _anim.SetBool(_hashRush, true);

        _startPos = transform.position;
        _isRush = true;

        direction = transform.forward;

        _rb.isKinematic = false;
    }

    public override TaskStatus OnUpdate()
    {
        _owner.RushAttack(_dashSpeed.Value, direction);
        //_rb.velocity = direction * _dashSpeed.Value;

        if (_isRush)
        { 
            float distance = Vector3.Distance(transform.position, _startPos);

            if (distance >= _dashRange.Value)
            {
                _isRush = false;
            }
        }

        if (!_isRush)
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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _owner.player.Hit(_owner.rushDamage, 0f, _owner.transform);
                _owner.isPlayerHit = true;
            }

            _isRush = false;
        }     
    }
}
