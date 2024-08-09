using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossPhase
{ 
    Phase1, Phase2
}

public class BossController : MonoBehaviour
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackRange;
    private float _hpPercent;

    private Rigidbody _rb;
    private Animator _anim;
    private NavMeshAgent _nav;

    private BossPhase _phase;

    private Transform _playerTr;

    private readonly int _hashPhase = Animator.StringToHash("");
    private readonly int _hashAttack = Animator.StringToHash("");
    private readonly int _hashAttackPattern = Animator.StringToHash("");
    private readonly int _hashSkill = Animator.StringToHash("");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();

        _playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        _hpPercent = _hp / _maxHp;
    }

    private void OnEnable()
    {
        _phase = BossPhase.Phase1;

        _hp = _maxHp;
    }

    public bool CheckDistance(float range)
    {
        if (_playerTr == null) return false;
        
        float distance = Vector3.Distance(transform.position, _playerTr.position);

        if (distance <= range) return true;

        return false;
    }

    public bool CheckPhase(float standard)
    {
        if (standard <= _hpPercent) return true;

        return false;
    }
}
