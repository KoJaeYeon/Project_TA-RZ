using System;
using UnityEngine;
using BehaviorDesigner.Runtime;

using Zenject;
using System.Collections;

public class Monster : MonoBehaviour
{
    [SerializeField] BehaviorTree Bt;
    [SerializeField] int Mon_Common_Stat_Hp;
    [SerializeField] float Mon_Common_Damage;
    [SerializeField] float Mon_Common_AttackArea;
    [SerializeField] float Mon_Common_Range;
    [SerializeField] float Mon_Common_DetectArea;
    [SerializeField] float Mon_Common_DetectTime;
    [SerializeField] float Mon_Common_MovementSpeed;
    public float Mon_Common_CoolTime;

    [Inject] public Player Player { get;}

    void Start()
    {
        Bt = GetComponent<BehaviorTree>();
    }
    private void Update()
    {

       
    }


    
}
