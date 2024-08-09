using System;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public enum MonsterType
{
    CommonType,
    ExplosionType,
    LongRangeType,
    RushType,

}
public class Monster : MonoBehaviour
{
    [Header("적 타입")]
    [SerializeField] MonsterType Type;

    [SerializeField] BehaviorTree Bt;
    [SerializeField] int MonsterHp;

    void Start()
    {
        switch (Type)
        {
            case MonsterType.CommonType:
                
                Debug.Log("Common");
                break;
            case MonsterType.LongRangeType:
                Debug.Log("LongRange");
                break;
            case MonsterType.ExplosionType:
                Debug.Log("ExplosionType");
                break;
            case MonsterType.RushType:

                break;
        }
        
        
        
    }
    private void Update()
    {

       
    }

}
