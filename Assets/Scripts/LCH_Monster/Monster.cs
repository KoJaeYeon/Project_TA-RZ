using System;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Zenject;
using System.Collections;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IHit
{
    [Inject] public Player Player { get;}
    [Inject] DataManager dataManager;
    BehaviorTree Bt;
    NavMeshAgent Nav;

    Rigidbody _rigidbody;

    public float Mon_Common_Stat_Hp;
    public float Mon_Common_Damage;
    public float Mon_Common_AttackArea;
    public float Mon_Common_Range;
    public float Mon_Common_DetectArea;
    public float Mon_Common_DetectTime;
    public float Mon_Common_MovementSpeed;
    public float Mon_Common_CoolTime;
    public float Mon_Knockback_Time;
    public float Mon_Knockback_Speed;
    public bool isDamaged;
    public bool isAtk;
    public bool isKnockBack;
    public float Mon_Common_Hp_Remain;

    public float targetTime;

    protected Monster_Stat monster_Stat = new Monster_Stat();
    protected string idStr = "E101";

    Coroutine _hitCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Bt = GetComponent<BehaviorTree>();
        Nav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Mon_Common_Hp_Remain = Mon_Common_Stat_Hp;
        StartCoroutine(LoadStat());
    }

    IEnumerator LoadStat()
    {
        while (true)
        {
            var stat = dataManager.GetStat(idStr) as Monster_Stat;
            if (stat == null)
            {
                Debug.Log($"Monster[{idStr}]의 스탯을 받아오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                monster_Stat = stat;
                Debug.Log("Monster[{idStr}]의 스탯을 성공적으로 받아왔습니다.");
                Mon_Common_Stat_Hp = monster_Stat.HP;
                Mon_Common_Hp_Remain = monster_Stat.HP;
                Mon_Common_Damage = monster_Stat.Damage;
                Mon_Common_AttackArea = monster_Stat.AttackArea;
                Mon_Common_Range = monster_Stat.Range;
                Mon_Common_DetectArea = monster_Stat.DetectArea;
                Mon_Common_DetectTime = monster_Stat.DetectTime;
                Mon_Common_MovementSpeed = monster_Stat.MovementSpeed;
                yield break;
            }

        }
    }

    public void Hit(float damage, float paralysisTime, Transform transform)
    {
        isDamaged = true;
        Mon_Common_Hp_Remain -= damage;

        if (Mon_Common_Hp_Remain > 0)
        {
            if(_hitCoroutine != null)
            {
                StopCoroutine(_hitCoroutine);
            }
            _hitCoroutine = StartCoroutine(WaitForStun(paralysisTime));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ApplyKnockback(float knockbackDuration, Transform attackerTrans)
    {
        isKnockBack = true;

        Nav.enabled = false;

        _rigidbody.velocity = Vector3.zero;

        Vector3 knockBackDirection = transform.position - attackerTrans.position;

        knockBackDirection.y = 0;

        knockBackDirection.Normalize();

        Vector3 knockBack = knockBackDirection * 5f + Vector3.up * Mon_Knockback_Speed;

        _rigidbody.AddForce(knockBack, ForceMode.Impulse);
    }

    public void Attack()
    {
        Player.Hit(Mon_Common_Damage, 1, transform);
    }

    public IEnumerator WaitForStun(float paralysisTime)
    {
        //Bt.enabled = false;
        var anim = GetComponent<Animator>();
        anim.SetTrigger("Damaged");
        yield return new WaitForSeconds(paralysisTime);
        isDamaged = false;
        if (!isDamaged)
        {
           // Bt.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, Mon_Common_AttackArea);
    }
}
