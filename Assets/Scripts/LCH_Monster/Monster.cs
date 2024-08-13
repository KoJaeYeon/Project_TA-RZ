using System;
using UnityEngine;
using BehaviorDesigner.Runtime;

using Zenject;
using System.Collections;

public class Monster : MonoBehaviour
{
    [SerializeField] BehaviorTree Bt;
    public int Mon_Common_Stat_Hp;
    public float Mon_Common_Damage;
    public float Mon_Common_AttackArea;
    public float Mon_Common_Range;
    public float Mon_Common_DetectArea;
    public float Mon_Common_DetectTime;
    public float Mon_Common_MovementSpeed;
    public float Mon_Common_CoolTime;

    [Inject] public Player Player { get;}

    void Start()
    {
        Bt = GetComponent<BehaviorTree>();
    }



    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag())
    }
}
