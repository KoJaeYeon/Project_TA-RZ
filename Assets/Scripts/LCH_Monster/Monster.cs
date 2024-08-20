using System;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Zenject;
using System.Collections;

public class Monster : MonoBehaviour, IHit
{
    [SerializeField] BehaviorTree Bt;
    public float Mon_Common_Stat_Hp;
    public float Mon_Common_Damage;
    public float Mon_Common_AttackArea;
    public float Mon_Common_Range;
    public float Mon_Common_DetectArea;
    public float Mon_Common_DetectTime;
    public float Mon_Common_MovementSpeed;
    public float Mon_Common_CoolTime;

    public bool isDamaged;
    public bool isAtk;
    public bool isKnockBack;
    public float Mon_Common_Hp_Remain;
    [Inject] public Player Player { get;}
    [Inject] DataManager dataManager;

    protected Monster_Stat monster_Stat = new Monster_Stat();
    protected string idStr = "E101";

    void Start()
    {
        Mon_Common_Hp_Remain = Mon_Common_Stat_Hp;
        Bt = GetComponent<BehaviorTree>();
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
            StartCoroutine(WaitForStun(paralysisTime));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ApplyKnockback(float knockbackForce, Transform attackerTrans)
    {
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 knockbackDirection = (transform.position - attackerTrans.position).normalized;

            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }

    public void Attack()
    {
        string[] targetLayers = new string[] { "Player", "Dash", "Ghost" };
        int layer = LayerMask.GetMask(targetLayers);
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, Mon_Common_AttackArea, layer);

        if(colliders.Length == 0) return;

        var player = colliders[0].gameObject;
        var ihit = player.GetComponent<IHit>();
        ihit?.Hit(Mon_Common_Damage, 1, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("데미지 받음");
            isKnockBack = true;
            Hit(10, 5, transform);
            //ApplyKnockback(Player.gameObject.transform.position, 1f); 
        }
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
