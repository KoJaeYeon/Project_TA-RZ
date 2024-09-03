using UnityEngine;
using BehaviorDesigner.Runtime;
using Zenject;
using System.Collections;
using UnityEngine.AI;
using TMPro;
public enum MonsterAbility
{
    Original,
    Speed,
    Power,
    Shield,
}
public enum MonsterType
{
    Basic,
    Supply,
    Few
}
public class Monster : MonoBehaviour, IHit
{
    [Header("디버깅용")]
    [SerializeField] bool test;
    [Header("몬스터 타입")]
    [SerializeField] MonsterType Type;
    [SerializeField] MonsterAbility _Ability;
    [Inject] public Player Player { get;}
    [Inject] DataManager _dataManager;
    BehaviorTree _bt;
    NavMeshAgent Nav;
    Rigidbody _rigidbody;
    Stage _stage;

    public Animator Anim { get; set; }
    public float Mon_Common_Stat_Hp { get; set; }
    public float Mon_Common_Damage { get; set; }
    public float Mon_Common_AttackArea { get; set; }
    public float Mon_Common_Range { get; set; }
    public float Mon_Common_DetectArea { get; set; }
    public float Mon_Common_DetectTime { get; set; }
    public float Mon_Common_MovementSpeed { get; set; }
    public float Mon_Common_CoolTime { get; set; }
    public float Mon_Knockback_Time { get; set; }
    public bool IsCollsion { get; set; } = false;
    public bool IsDamaged { get; set; }
    public bool IsAtk { get; set; }
    public bool IsKnockBack { get; set; }
    public float Mon_Common_Hp_Remain { get; set; }
    private bool _isSpawn = false;

    [Header("공격 경직시간 조절")]
    [SerializeField] float Attack_Stiff_Time = 1;
    [Header("넉백 조절")]
    [SerializeField] float Knockback_Horizontal_Distance = 4f;
    [SerializeField] float Knockback_Vertical_Distance = 2f;

    public float ApplyingKnockbackTime { get; set; }
    public float ApplyingStiffTime { get; set; }

    protected Monster_Stat monster_Stat = new Monster_Stat();
    protected string idStr = "E101";

    Coroutine _hitCoroutine;

   // [Header("개발자 인스펙터")]
    //[SerializeField] TextMeshProUGUI TempHPText;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _bt = GetComponent<BehaviorTree>();
        Nav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (Type == MonsterType.Supply)
        {
            gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        if (Type == MonsterType.Few)
        {
            gameObject.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        }
        Mon_Common_Hp_Remain = Mon_Common_Stat_Hp;
        if (test == true)
        {
            OnSetMonsterStat(1f);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            ApplyKnockback(2, Player.transform);
        }
        Debug.Log($"몬스터의 쿨타임? : { Mon_Common_CoolTime}");
    }

    /// <summary>
    /// 몬스터 생성 시 스탯을 결정해주는 함수
    /// </summary>
    /// <param name="monsterAbility"></param>
    /// <param name="monsterType"></param>
    public virtual void OnSetMonsterStat(float stat_Multiplier)
    {
        int rand = Random.Range(0, 3);
        transform.GetChild(rand).gameObject.SetActive(true);
        _Ability = (MonsterAbility)rand;
        Anim = GetComponentInChildren<Animator>();

        StartCoroutine(LoadStat(stat_Multiplier));
    }

    IEnumerator LoadStat(float stat_Multiplier)
    {
        while (true)
        {
            var stat = _dataManager.GetStat(idStr) as Monster_Stat;
            ///나중에 따로 스탯이 생기면 몬스터의 배율을 적용해 줄 부분
            var data = _dataManager.GetData($"E21{(int)_Ability + 1}") as Monster_Ability;
            if (stat == null)
            {
                Debug.Log($"Monster[{idStr}]의 스탯을 받아오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                monster_Stat = stat;
                Debug.Log("Monster[{idStr}]의 스탯을 성공적으로 받아왔습니다.");
                

                Mon_Common_Stat_Hp = monster_Stat.HP * data.Stat_HPMag * stat_Multiplier;
                Mon_Common_Hp_Remain = Mon_Common_Stat_Hp;
                Mon_Common_Damage = monster_Stat.Damage * data.Stat_DmgMag * stat_Multiplier;
                Mon_Common_AttackArea = monster_Stat.AttackArea;
                Mon_Common_Range = monster_Stat.Range;
                Mon_Common_DetectArea = monster_Stat.DetectArea;
                Mon_Common_DetectTime = monster_Stat.DetectTime;
                Mon_Common_MovementSpeed = monster_Stat.MovementSpeed * data.Stat_MSMag * stat_Multiplier;
                Mon_Common_CoolTime = monster_Stat.Cooldown * data.Stat_CDMag;
                //TempHPText.text = Mon_Common_Hp_Remain.ToString();
                yield break;
            }
            
        }
    }

    public virtual void Hit(float damage, float paralysisTime, Transform transform)
    {
        IsDamaged = true;
        Mon_Common_Hp_Remain -= damage;
        //TempHPText.text = Mon_Common_Hp_Remain.ToString();

        Debug.Log("hit");

        if (Mon_Common_Hp_Remain > 0)
        {
            if(_hitCoroutine != null)
            {
                StopCoroutine(_hitCoroutine);
            }
            if(paralysisTime == 0)
            {
                IsDamaged = false;
                return;
            }
            _hitCoroutine = StartCoroutine(WaitForStun(paralysisTime));
        }
        else
        {
            if (_isSpawn)
            {
                _isSpawn = false;

                _stage.UnRegisterMonster(this.gameObject);
            }
            gameObject.SetActive(false);
        }
    }

    public virtual void ApplyKnockback(float knockbackDuration, Transform attackerTrans)
    {
        IsKnockBack = true;

        _rigidbody.isKinematic = false;

        gameObject.layer = LayerMask.NameToLayer("MonsterKnockback");

        Anim.SetTrigger("Damaged");

        ApplyingKnockbackTime = Time.time + knockbackDuration;

        Nav.enabled = false;

        _rigidbody.velocity = Vector3.zero;

        Vector3 knockBackDirection = transform.position - attackerTrans.position;

        knockBackDirection.y = 0;

        knockBackDirection.Normalize();

        Vector3 knockBack = (knockBackDirection * Knockback_Horizontal_Distance + Vector3.up * Knockback_Vertical_Distance);

        _rigidbody.AddForce(knockBack, ForceMode.Impulse);
    }

    public void KnockbackEnd()
    {
        IsKnockBack = false;
        _rigidbody.isKinematic = true;
        IsCollsion = false;
        gameObject.layer = LayerMask.NameToLayer("Monster");
    }

    public void Attack()
    {
        Player.Hit(Mon_Common_Damage, Attack_Stiff_Time, transform);
    }

    public IEnumerator WaitForStun(float paralysisTime)
    {
        //Bt.enabled = false;
        Anim.SetTrigger("Damaged");
        yield return new WaitForSeconds(paralysisTime);
        IsDamaged = false;
    }

    public void IsSpawn(Stage stage)
    {
        if (!_isSpawn)
        {
            _isSpawn = true;
            _stage = stage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Shield"))
        {
            IsCollsion = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, Mon_Common_AttackArea);
    }

}
