using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using Zenject;
using UnityEngine.UI;

public enum BossPhase
{ 
    Phase1, Phase2
}

public class BossController : MonoBehaviour, IHit
{
    private GameObject _bossPrefab;
    [SerializeField] Boss_MarkManager markManager;
    [SerializeField] Boss_DamageBoxManager damageBoxManager;
    [Inject] DataManager dataManager { get; }
    [Inject] public Player player { get; }
    [Inject] private Boss_ParticleManager _boss_Particle;

    [Header("기본 정보")]
    [Tooltip("현재 페이즈")] public BossPhase phase;
    [Tooltip("최대 체력")] public float _maxHp;
    [SerializeField, Tooltip("현재 체력")] private float _hp;                  
    [HideInInspector] public float _hpPercent;
    [SerializeField, Tooltip("공격 가능 사거리")] private float _attackRange;
    [SerializeField, Tooltip("대쉬 체크용 사거리")] private float _rushDistance;
    [SerializeField, Tooltip("추적 사거리")] private float _traceDistance;
    [SerializeField, Tooltip("회전 속도")] private float _rotSpeed;

    [Header("페이즈 체력 퍼센트")]
    [SerializeField] private float _phaseOnePer;
    [SerializeField] private float _phaseTwoPer;

    [Header("1페이즈 기믹")]
    [SerializeField] private GimmickController _gimmick;
    [SerializeField] private GameObject _gimmickDamageBox;
    public float gimmickDamage;
    [SerializeField] private float _gimmickCoolDown;
    public bool isCoolGimmick;
    [Header("1페이즈 뿌리 공격")]
    [SerializeField] private GameObject _markRoot;
    [SerializeField] private GameObject _rootDamageBox;
    [SerializeField] private RootContoller[] _roots;
    public float rootDamage;
    [SerializeField] private float _rootCoolDown;
    public bool isCoolRootAttack;
    [Header("1, 2페이즈 내려치기")]
    public float smashDamage;
    [SerializeField] private float _smashCoolDown;
    public bool isCoolSmash;
    [Header("1페이즈 폭발")]
    [SerializeField] private GameObject[] _firstExplosion;
    [SerializeField] private GameObject[] _firstExplosionDamageBox;
    [SerializeField] private Transform[] _firstExplosionTr;
    [SerializeField] private GameObject _secondExplosion;
    [SerializeField] private GameObject _secondExplosionDamageBox;
    [SerializeField] private Transform[] _secondExplosionTr;
    [SerializeField] private float _explosionTime;
    public float explosionDamage;
    [SerializeField] private float _explosionCoolDown;
    public bool isCoolExplosion;

    [Header("2페이즈 돌진공격")]
    [SerializeField] private GameObject _rushMark;
    [SerializeField] private ParticleSystem _rushPar;
    [SerializeField] private float _rushSpeed;
    public float rushRange;
    public float rushWailtTime;
    [SerializeField] private float _defaultRushTime;
    public float rushDamage;
    [SerializeField] private float _rushCoolDown;
    public bool isCoolRush;
    [Header("2페이즈 휘두르기")]
    [SerializeField] private GameObject _swingMark;
    [SerializeField] private GameObject _swingDamageBox;
    public float swingDamage;
    [SerializeField] private float _swingCoolDown;
    public bool isCoolSwing;
    [Header("2페이즈 내려치기")]
    [SerializeField] private GameObject _smashMark;
    [SerializeField] private GameObject _smashDamageBox;

    private Dictionary<string,Boss_Skill> _boss_Skill = new Dictionary<string, Boss_Skill>();


    private Rigidbody _rb;
    private Animator _anim;
    private NavMeshAgent _nav;
    private BehaviorTree _bt;

    private Transform _playerTr;

    public bool isGimmick;
    public bool isPlayerHit = false;

    [SerializeField] private Slider _hpBar;

    private readonly int _hashPhase = Animator.StringToHash("");
    private readonly int _hashAttack = Animator.StringToHash("");
    private readonly int _hashAttackPattern = Animator.StringToHash("");
    private readonly int _hashSkill = Animator.StringToHash("");

    public enum Pattern
    {
        gimmick,
        rootAttack,
        smash,
        explosion,
        rush,
        swing,
        explosion2
    }

    private void Awake()
    {
        _bossPrefab = transform.parent.gameObject;

        _gimmick = markManager.mark_Gimmick.GetComponent<GimmickController>();
        _markRoot = markManager.mark_RootAttack;
        _firstExplosion = markManager.mark_FirstExplosion;
        _secondExplosion = markManager.mark_SecondExplosion;
        _rushMark = markManager.mark_Rush;
        _rushPar = markManager.mark_RushPar;
        _swingMark = markManager.mark_Swing;
        _smashMark = markManager.mark_smash;

        _gimmickDamageBox = damageBoxManager.damageBox_Gimmick;
        _rootDamageBox = damageBoxManager.damageBox_RootAttack;
        _firstExplosionDamageBox = damageBoxManager.damageBox_FirstExplosion;
        _secondExplosionDamageBox = damageBoxManager.damageBox_SecondExplosion;
        _swingDamageBox = damageBoxManager.damageBox_Swing;
        _smashDamageBox = damageBoxManager.damageBox_smash;

        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        _bt = GetComponent<BehaviorTree>();

        _playerTr = player.transform;

        _gimmick.gameObject.SetActive(false);
        _markRoot.SetActive(false);
        foreach (var explosion in _firstExplosion) 
        { 
            explosion.SetActive(false);
        }
        _secondExplosion.SetActive(false);
        _rushMark.SetActive(false);
        _rushPar.gameObject.SetActive(false);
        _swingMark.SetActive(false);
        _smashMark.SetActive(false);

        _gimmickDamageBox.SetActive(false);
        _rootDamageBox.SetActive(false);
        foreach (var item in _firstExplosionDamageBox)
        { 
            item.SetActive(false);
        }
        _secondExplosionDamageBox.SetActive(false);
        _swingDamageBox.SetActive(false);
        _smashDamageBox.SetActive(false);

        _bt.SetVariableValue("BossPrefab", _bossPrefab);

        _bt.SetVariableValue("Phase1_Per", _phaseOnePer);
        _bt.SetVariableValue("Phase2_Per", _phaseTwoPer);
        _bt.SetVariableValue("Attack_Distance", _attackRange);
        _bt.SetVariableValue("Rush_Distance", _rushDistance);
        _bt.SetVariableValue("Trace_Distance", _traceDistance);
        _bt.SetVariableValue("Root_Distance", _roots[0]._attackRange);

        _bt.SetVariableValue("RushSpeed", _rushSpeed);
        _bt.SetVariableValue("RushRange", rushRange);
        _bt.SetVariableValue("RushWaitTime", rushWailtTime);
        _bt.SetVariableValue("DefaultRushWaitTime", _defaultRushTime);

        #region 테스트

        _maxHp = 3000;

        #endregion 테스트

        StartCoroutine(LoadStat());
    }

    private void OnEnable()
    {
        phase = BossPhase.Phase1;

        _hp = _maxHp;
        _hpPercent = _hp / _maxHp * 100;
        _hpBar.value = _hpPercent;

        StartCoroutine(CoCheckCoolTime(_gimmickCoolDown, Pattern.gimmick));
    }

    #region 테스트
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _hp = 1000;
            _hpPercent = _hp / _maxHp * 100;
        }
    }
    #endregion 테스트

    #region BTA

    #region Phase1

    //기믹 범위 표시
    public void MarkGimmick()
    { 
        _gimmick.transform.position = transform.position;
        SetYPosition(_gimmick.transform);
        _gimmick.transform.rotation = transform.rotation;
        _gimmick.gameObject.SetActive(true);
        _boss_Particle.OnGimmickCharging(transform);
        isGimmick = true;
    }
    //기믹 실행부
    public void ActiveGimmick()
    { 
        _gimmick.gameObject.SetActive(false);
        CallDamageBox(Pattern.gimmick);
        _boss_Particle.OnGimmickExplosion(transform);
        isGimmick = false;
        StartCoroutine(CoCheckCoolTime(_gimmickCoolDown, Pattern.gimmick));
    }
    //뿌리공격 범위 표시
    public void MarkActiveRoot()
    { 
        _markRoot.gameObject.transform.position = _playerTr.position;
        _markRoot.gameObject.SetActive(true);
    }
    //뿌리공격 실행부
    public void ActiveRoot()
    {
        _markRoot.gameObject.SetActive(false);
        CallDamageBox(Pattern.rootAttack);
        _boss_Particle.OnRootAttack(_markRoot.transform);
        foreach (var root in _roots)
        {
            if (root.rootState == RootState.Emerge)
                continue;

            if (root.rootState == RootState.Die)
                continue;

            root.rootState = RootState.Emerge;
            root.RootAttack(_markRoot.gameObject.transform.position + Vector3.one * 0.1f);
            break;
        }
        StartCoroutine(CoCheckCoolTime(_rootCoolDown, Pattern.rootAttack));
    }
    //내려치기 범위표시
    public void MarkRootSmash()
    {
        foreach (var root in _roots)
        {
            if (root.rootState == RootState.Hide)
                continue;

            if (root.rootState == RootState.Die)
                continue;

            root.MarkSmash(_playerTr.position);
        }
    }
    //내려치기 실행부
    public void RootSmash()
    {
        foreach (var root in _roots)
        {
            if (root.rootState == RootState.Hide)
                continue;

            if (root.rootState == RootState.Die)
                continue;

            root.RootSmash();
        }
        StartCoroutine(CoCheckCoolTime(_smashCoolDown, Pattern.smash));
    }
    //폭발 공격1 범위표시
    public void MarkFirstExplosion()
    {
        foreach (var explosion in _firstExplosion)
        {
            explosion.transform.position = transform.position;
            SetYPosition(explosion.transform);
            explosion.SetActive(true);
        }
    }
    //폭발 공격1 실행부
    public void FirstExplosion()
    {
        foreach (var explosion in _firstExplosion)
        {
            explosion.SetActive(false);
        }

        _boss_Particle.OnFirstExplosion(_firstExplosionTr);

        _secondExplosion.transform.position = transform.position;
        SetYPosition(_secondExplosion.transform);
        _secondExplosion.SetActive(true);
    }
    //폭발 공격2 실행부
    public void SecondExplosion()
    {
        _secondExplosion.SetActive(false);

        _boss_Particle.OnSecondExplosion(_secondExplosionTr);

        StartCoroutine(CoCheckCoolTime(_explosionCoolDown, Pattern.explosion));
    }

    #endregion Phase1

    //보스 회전 관련
    public void LookAtPlayer(Quaternion rot)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, _rotSpeed * Time.deltaTime);
    }
    public Quaternion PlayerRot()
    {
        return RotateToPlayer();
    }
    public Vector3 PlayerPos()
    {
        return _playerTr.position;
    }

    #region Phase2

    //Phase1 잔재 삭제
    public void ChangePhaseTwo()
    {
        phase = BossPhase.Phase2;
        this.gameObject.tag = "Monster";
    }

    //보스 대쉬 공격
    public void DrawRushMark()
    {
        rushWailtTime -= _defaultRushTime * 0.2f;
        _bt.SetVariableValue("RushWaitTime", rushWailtTime);
        _rushMark.transform.position = transform.position;
        SetYPosition(_rushMark.transform);
        _rushMark.transform.position = _rushMark.transform.position + Vector3.up * 0.01f;
        _rushMark.transform.rotation = transform.rotation;
        _rushMark.SetActive(true);

        _rushPar.transform.position = transform.position;
        SetYPosition(_rushPar.transform);
        _rushPar.transform.position = _rushPar.transform.position + Vector3.up * 0.01f;
        _rushPar.transform.rotation = transform.rotation;
        _rushPar.gameObject.SetActive(true);
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
        _rushMark.SetActive(false);
        _rushPar.gameObject.SetActive(false);
        _rb.velocity = direction * speed;
    }
    public void RushCool()
    {
        StartCoroutine(CoCheckCoolTime(_rushCoolDown, Pattern.rush));
    }
    public void ResetRushWaitTime()
    {
        rushWailtTime = _defaultRushTime;
    }

    //휘두르기
    public void DrawSwing(bool isActive)
    {
        _swingMark.transform.position = transform.position;
        SetYPosition(_swingMark.transform);
        _swingMark.transform.rotation = transform.rotation;
        _swingMark.SetActive(isActive);
    }
    public void SwingCool()
    {
        StartCoroutine(CoCheckCoolTime(_swingCoolDown, Pattern.swing));
    }

    //내려치기
    public void DrawSmash(bool isActive)
    {
        _smashMark.transform.position = transform.position;
        SetYPosition(_smashMark.transform);
        _smashMark.transform.position = _smashMark.transform.position + Vector3.up * 0.01f;
        _smashMark.transform.rotation = transform.rotation;
        _smashMark.SetActive(isActive);
    }
    public void SmashCool()
    {
        StartCoroutine(CoCheckCoolTime(_smashCoolDown, Pattern.smash));
    }

    #endregion Phase2

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

    //쿨타임 체크
    public bool CheckCoolDown(bool isPattern)
    {
        if(_playerTr == null) return false;

        if(!isPattern) return true;

        return false;
    }
    //뿌리 활성화 확인 (전부 활성화 되있는지)
    public bool CheckAllRoot()
    { 
        if (_playerTr == null) return false;

        int count = 0;

        foreach (var root in _roots)
        {
            if (root.rootState == RootState.Emerge || root.rootState == RootState.Die)
            {
                count++;
            }
        }

        if (count < _roots.Length) return true;

        return false;
    }
    //뿌리 활성화 확인
    public bool CheckRootActive()
    {
        if (_playerTr == null) return false;

        foreach (var root in _roots)
        {
            if (root.rootState == RootState.Emerge)
            { 
                return true;
            }
        }

        return false;
    }

    //뿌리 거리 체크
    public bool CheckRootDistance(float range)
    {
        if (_playerTr == null) return false;

        foreach (var root in _roots)
        {
            float distance = Vector3.Distance(root.gameObject.transform.position, _playerTr.position);

            if (distance <= range) return true;
        }

        return false;
    }

    #endregion BTC

    //플레이어 방향
    private Quaternion RotateToPlayer()
    {
        Vector3 direction = (_playerTr.position - transform.position);
        direction.y = 0;
        direction.Normalize();
        Quaternion rotation = Quaternion.LookRotation(direction);
        return rotation;
    }

    //데미지 입는 메서드
    public void Hurt(float damage)
    {
        if (phase == BossPhase.Phase1 && _gimmick.gameObject.activeSelf) _gimmick.OnActivateCombat();

        if (_hp - damage <= 0)
        {
            _hp = 0;
            _hpPercent = _hp / _maxHp * 100;
            _hpBar.value = Mathf.Lerp(_hpBar.value, _hpPercent, Time.deltaTime * damage);
            return;
        }

        _hp -= damage;
        _hpPercent = _hp / _maxHp * 100;
        _hpBar.value = Mathf.Lerp(_hpBar.value, _hpPercent, Time.deltaTime * damage);
    }

    //스킬 쿨타임 셋팅해주는 메서드
    private void SetBoolCooldown(Pattern pattern, bool isCool)
    {
        switch (pattern)
        {
            case Pattern.gimmick:
                isCoolGimmick = isCool;
                break;

            case Pattern.rootAttack:
                isCoolRootAttack = isCool;
                break;

            case Pattern.smash:
                isCoolSmash = isCool;
                break;

            case Pattern.explosion:
                isCoolExplosion = isCool;
                break;

            case Pattern.rush:
                isCoolRush = isCool;
                break;

            case Pattern.swing:
                isCoolSwing = isCool;
                break;

            default:
                Debug.Log("해당사항없음");
                break;
        }
    }
    //각 쿨타임 시간이후 쿨타임 체크 해제
    private IEnumerator CoCheckCoolTime(float time, Pattern pattern)
    {
        SetBoolCooldown(pattern, true);
        yield return new WaitForSeconds(time);
        SetBoolCooldown(pattern, false);
    }

    //데미지 박스 위치값 초기화
    private void SetTransformDamageBox(Pattern pattern)
    {
        switch (pattern)
        {
            case Pattern.gimmick:
                _gimmickDamageBox.transform.position = transform.position;
                SetYPosition(_gimmickDamageBox.transform);
                _gimmickDamageBox.transform.rotation = transform.rotation;
                _gimmickDamageBox.transform.localScale = _gimmick.transform.localScale;
                break;

            case Pattern.rootAttack:
                _rootDamageBox.transform.position = _markRoot.transform.position;
                SetYPosition(_rootDamageBox.transform);
                _rootDamageBox.transform.rotation = _markRoot.transform.rotation;
                break;

            case Pattern.smash:
                _smashDamageBox.transform.position = transform.position;
                SetYPosition(_smashDamageBox.transform);
                _smashDamageBox.transform.rotation = transform.rotation;
                break;

            case Pattern.explosion:
                foreach (var item in _firstExplosionDamageBox)
                {
                    item.transform.position = transform.position;
                    SetYPosition(item.transform);
                    item.transform.rotation = transform.rotation;
                }
                break;

            case Pattern.explosion2:
                _secondExplosionDamageBox.transform.position = transform.position;
                SetYPosition(_secondExplosionDamageBox.transform);
                _secondExplosionDamageBox.transform.rotation = transform.rotation;
                break;

            case Pattern.swing:
                _swingDamageBox.transform.position = transform.position;
                SetYPosition(_swingDamageBox.transform);
                _swingDamageBox.transform.rotation = transform.rotation;
                break;

            default:
                Debug.Log("해당사항없음");
                break;
        }
    }
    //데미지 박스 활성화, 비활성화
    private void SetActiveDamageBox(Pattern pattern, bool isActive)
    {
        switch (pattern)
        {
            case Pattern.gimmick:
                _gimmickDamageBox.SetActive(isActive);
                break;

            case Pattern.rootAttack:
                _rootDamageBox.SetActive(isActive);
                break;

            case Pattern.smash:
                _smashDamageBox.SetActive(isActive);
                break;

            case Pattern.explosion:
                foreach (var item in _firstExplosionDamageBox)
                {
                    item.SetActive(isActive);
                }
                break;

            case Pattern.explosion2:
                _secondExplosionDamageBox.SetActive(isActive);
                break;

            case Pattern.swing:
                _swingDamageBox.SetActive(isActive);
                break;

            default:
                Debug.Log("해당사항없음");
                break;
        }
    }
    //데미지 박스 활성화 후 일정 시간 뒤에 비활성화 시켜주는 코루틴
    private IEnumerator CoSetActiveDamageBox(Pattern pattern, float time)
    {
        SetTransformDamageBox(pattern);
        SetActiveDamageBox(pattern, true);
        yield return new WaitForSeconds(time);
        SetActiveDamageBox(pattern, false);
    }
    //패턴별 코루틴 호출 메서드
    private float time = 0.2f;
    public void CallDamageBox(Pattern pattern)
    {
        switch (pattern)
        {
            case Pattern.gimmick:
                StartCoroutine(CoSetActiveDamageBox(Pattern.gimmick, time));
                break;

            case Pattern.rootAttack:
                StartCoroutine(CoSetActiveDamageBox(Pattern.rootAttack, time));
                break;

            case Pattern.explosion:
                StartCoroutine(CoSetActiveDamageBox(Pattern.explosion, time));
                break;

            case Pattern.explosion2:
                StartCoroutine(CoSetActiveDamageBox(Pattern.explosion2, time));
                break;

            case Pattern.swing:
                StartCoroutine(CoSetActiveDamageBox(Pattern.swing, time));    //애니메이션 이벤트로 실행
                break;

            case Pattern.smash:
                StartCoroutine(CoSetActiveDamageBox(Pattern.smash, time));    //애니메이션 이벤트로 실행
                break;
        }
    }

    //IHit 인터페이스 메서드
    public void Hit(float damage, float paralysisTime, Transform attackTrans)
    {
        if (phase == BossPhase.Phase1)
        {
            if (_gimmick.gameObject.activeSelf)
            {
                _gimmick.OnActivateCombat();
            }
            Debug.Log("Phase1 Hit");
            return;
        }

        Hurt(damage);
    }
    public void ApplyKnockback(float knockBackTime, Transform otherPosition)
    { 

    }

    //데이터 드리븐
    IEnumerator LoadStat()
    {
        yield return new WaitWhile(() =>
        {
            Debug.Log("Player의 데이터를 받아오는 중입니다.");
            return dataManager.GetData("B101") == null;
        });

        for (int i = 0; i < 6; i++)
        {
            string IDStr = $"B10{1 + i}";
            var data = dataManager.GetData(IDStr) as Boss_Skill;
            _boss_Skill.Add(IDStr,data);
        }

        {
            string IDStr = $"B201";
            var data = dataManager.GetData(IDStr) as Boss_Skill;
            _boss_Skill.Add(IDStr, data);
        }

        Debug.Log("Boss의 스킬 데이터를 성공적으로 받아왔습니다.");
        yield break;
    }

    private void SetYPosition(Transform tr)
    {
        Vector3 localPos = tr.localPosition;
        localPos.y = 0;
        tr.localPosition = localPos;
    }
}
