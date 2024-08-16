using System;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Zenject;
using System.Collections;

public class Monster : MonoBehaviour,IHit
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
    public float Mon_Common_Hp_Remain;
    [Inject] public Player Player { get;}

    void Start()
    {
        Mon_Common_Hp_Remain = Mon_Common_Stat_Hp;
        Bt = GetComponent<BehaviorTree>();
    }

    public void Hit(float damage, float paralysisTime)
    {
        isDamaged = true;
        Mon_Common_Hp_Remain -= damage;
        
        if (Mon_Common_Hp_Remain > 0)
        {
            StartCoroutine(WaitForStun(paralysisTime));
        }
    }
    public void ApplyKnockback(Vector3 otherPosition,float knockBackTime)
    {
        var knockback = GetComponent<Rigidbody>();
        knockback.AddForce(otherPosition);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("데미지받음");
            Hit(10,5);
            ApplyKnockback(this.gameObject.transform.position,10);
        }
    }

    public IEnumerator WaitForStun(float paralysisTime)
    {
        Bt.enabled = false;
        var anim = GetComponent<Animator>();
        anim.SetTrigger("Damaged");
        yield return new WaitForSeconds(paralysisTime);
        isDamaged = false;
        if (isDamaged == false)
        {
            Bt.enabled = true;
        }
    }
}
