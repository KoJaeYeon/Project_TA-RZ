using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public enum BossPhase
{ 
    Phase1, Phase2
}

public class BossController : MonoBehaviour
{
    [Header("기본 정보")]
    [SerializeField] private float _maxHp;
    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _rotSpeed;

    [Header("페이즈 체력")]
    [SerializeField] private float _phaseOnePer;
    [SerializeField] private float _phaseTwoPer;
    private float _hpPercent;

    [Header("2페이즈 돌진공격")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashRange;
        
    private Rigidbody _rb;
    private Animator _anim;
    private NavMeshAgent _nav;
    private BehaviorTree _bt;

    private BossPhase _phase;

    private Transform _playerTr;
    private TrailRenderer _trail;

    private readonly int _hashPhase = Animator.StringToHash("");
    private readonly int _hashAttack = Animator.StringToHash("");
    private readonly int _hashAttackPattern = Animator.StringToHash("");
    private readonly int _hashSkill = Animator.StringToHash("");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        _bt = GetComponent<BehaviorTree>();

        _playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _trail = GetComponentInChildren<TrailRenderer>();
        _trail.Clear();
        _trail.gameObject.SetActive(false);

        _bt.SetVariableValue("Phase1_Per", _phaseOnePer);
        _bt.SetVariableValue("Phase2_Per", _phaseTwoPer);
        _bt.SetVariableValue("Attack_Distance", _attackRange);

        _bt.SetVariableValue("DashSpeed", _dashSpeed);
        _bt.SetVariableValue("DashRange", _dashRange);

        #region 테스트

        _maxHp = 3000;

        #endregion
    }

    private void OnEnable()
    {
        _phase = BossPhase.Phase1;

        _hp = _maxHp;    
    }

    #region 테스트

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _hp = 1000;
        }
        _hpPercent = _hp / _maxHp * 100;
    }

    #endregion

    #region BTA

    public void LookAtPlayer()
    {
        Vector3 direction = (_playerTr.position - transform.position);
        direction.y = 0;
        direction.Normalize();
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotSpeed * Time.deltaTime);
    }
    public Quaternion PlayerRot()
    {
        Vector3 direction = (_playerTr.position - transform.position);
        direction.y = 0;
        direction.Normalize();
        Quaternion rotation = Quaternion.LookRotation(direction);
        return rotation;
    }

    public void DrawDashTrail()
    {
        _trail.gameObject.SetActive(true);
    }
    public void DashAttack(float speed)
    {
        Vector3 direction = (_playerTr.position - transform.position).normalized;
        _rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    #endregion

    #region BTC

    public bool CheckDistance(float range)
    {
        if (_playerTr == null) return false;
        
        float distance = Vector3.Distance(transform.position, _playerTr.position);

        if (distance <= range) return true;

        return false;
    }

    public bool CheckPhase(float standard)
    {
        if (standard < _hpPercent) return true;

        return false;
    }

    #endregion
}
