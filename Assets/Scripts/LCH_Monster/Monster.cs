using System;
using UnityEngine;
using BehaviorDesigner.Runtime;

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
    public float Mon_Common_CurrentHp;
    [Inject] public Player Player { get;}

    void Start()
    {
        Mon_Common_CurrentHp = Mon_Common_Stat_Hp;
        Bt = GetComponent<BehaviorTree>();
    }

    public void Hit(float damage)
    {
        isDamaged = true;
        Mon_Common_CurrentHp -= damage;
        StartCoroutine(WaitForStun());
        var anim = GetComponent<Animator>();
        anim.SetTrigger("Damaged");
        if (Mon_Common_CurrentHp <= 0)
        {

        }
    }
    public void ApplyKnockback()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("데미지받음");
            Hit(10);
        }
    }

    public IEnumerator WaitForStun()
    {
        Bt.enabled = false;
        yield return new WaitForSeconds(5);
        Bt.enabled = true;
        isDamaged = false;
    }
}
