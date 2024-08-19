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
    [Inject] public Player Player { get; }

    void Start()
    {
        Mon_Common_Hp_Remain = Mon_Common_Stat_Hp;
        Bt = GetComponent<BehaviorTree>();
    }

    public void Hit(float damage, float paralysisTime, Transform transform)
    {
        isDamaged = true;
        Mon_Common_Hp_Remain -= damage;

        if (Mon_Common_Hp_Remain > 0)
        {
            StartCoroutine(WaitForStun(paralysisTime));
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
}
