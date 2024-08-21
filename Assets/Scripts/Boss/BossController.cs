using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public enum BossPhase
{ 
    Phase1, Phase2
}

public struct BossPattern
{
    public string patternName;
    public float cooltime;

}

public class BossController : MonoBehaviour
{
    [Header("기본 정보")]
    public float _maxHp;
    [SerializeField] private float _hp;
    [HideInInspector] public float _hpPercent;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _rotSpeed;

    [Header("페이즈 체력 퍼센트")]
    [SerializeField] private float _phaseOnePer;
    [SerializeField] private float _phaseTwoPer;

    [Header("1페이즈 기믹")]
    public float gimmickDamage;
    private float _gimmickCoolDown;
    public bool isGimmick;
    [Header("1페이즈 뿌리 공격")]
    public float rootDamage;
    private float _rootCoolDown;
    public bool isRootAttack;
    [Header("1, 2페이즈 내려치기")]
    public float smashDamage;
    private float _smashCoolDown;
    public bool isSmash;
    [Header("1페이즈 폭발")]
    public float explosionDamage;
    private float _explotionCoolDown;
    public bool isExplosion;

    [Header("2페이즈 돌진공격")]
    [SerializeField] private float _rushSpeed;
    [SerializeField] private float _rushRange;
    public float rushDamage;
    private float _rushCoolDown;
    public bool isRush;
    [Header("2페이즈 휘두르기")]
    public float swingDamage;
    private float _swingCoolDown;
    public bool isSwing;
    //[Header("2페이즈 내려치기")]


    private Rigidbody _rb;
    private Animator _anim;
    private NavMeshAgent _nav;
    private BehaviorTree _bt;

    private BossPhase _phase;

    private Transform _playerTr;
    private TrailRenderer _trail;
    [Header("뿌리")]
    [SerializeField] private SpriteRenderer _markRoot;
    [SerializeField] private RootContoller[] _roots;
    [Header("폭발")]
    [SerializeField] private GameObject _explosion;

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
        _markRoot.gameObject.SetActive(false);

        _bt.SetVariableValue("Phase1_Per", _phaseOnePer);
        _bt.SetVariableValue("Phase2_Per", _phaseTwoPer);
        _bt.SetVariableValue("Attack_Distance", _attackRange);

        _bt.SetVariableValue("RushSpeed", _rushSpeed);
        _bt.SetVariableValue("RushRange", _rushRange);

        #region 테스트

        _maxHp = 3000;

        #endregion 테스트
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

    #endregion 테스트

    #region BTA

    #region Phase1

    public void MarkActiveRoot()
    {
        _markRoot.gameObject.transform.position = _playerTr.position;
        _markRoot.gameObject.SetActive(true);
    }
    public void ActiveRoot()
    {
        foreach (var root in _roots)
        {
            if (root.rootState == RootState.Emerge)
                continue;

            root.RootAttack(_markRoot.gameObject.transform.position);
            break;
        }
    }
    public void MarkSmash()
    {
        foreach (var root in _roots)
        {
            if (root.rootState == RootState.Hide)
                continue;

            root.MarkSmash(RotateToPlayer());
        }
    }
    public void RootSmash()
    {
        foreach (var root in _roots)
        {
            if (root.rootState == RootState.Hide)
                continue;

            root.RootSmash();
        }
    }
    public void Explosion()
    { 
        _explosion.SetActive(true);
    }

    #endregion Phase1

    //보스 회전 관련
    public void LookAtPlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, RotateToPlayer(), _rotSpeed * Time.deltaTime);
    }
    public Quaternion PlayerRot()
    {
        return RotateToPlayer();
    }
    public Vector3 PlayerPos()
    { 
        return _playerTr.position;
    }

    //보스 대쉬 공격
    public void DrawRushTrail()
    {
        _trail.gameObject.SetActive(true);
    }
    //방향 설정
    public Vector3 SetRushDirection()
    {
        Vector3 direction;
        direction = (_playerTr.position - transform.position);
        direction.y = 0;
        direction.Normalize();
        return direction;
    }
    //돌진
    public void RushAttack(float speed, Vector3 direction)
    {
        _rb.velocity = direction * speed;
    }

    #endregion BTA

    #region BTC

    //거리 체크
    public bool CheckDistance(float range)
    {
        if (_playerTr == null) return false;
        
        float distance = Vector3.Distance(transform.position, _playerTr.position);

        if (distance <= range) return true;

        return false;
    }

    //보스 Hp 체크
    public bool CheckPhase(float standard)
    {
        if (standard < _hpPercent) return true;

        return false;
    }

    #endregion BTC

    private Quaternion RotateToPlayer()
    {
        Vector3 direction = (_playerTr.position - transform.position);
        direction.y = 0;
        direction.Normalize();
        Quaternion rotation = Quaternion.LookRotation(direction);
        return rotation;
    }

    public void Hurt(float damage)
    {
        _hp -= damage;
        _hpPercent = _hp / _maxHp * 100;
    }
}
