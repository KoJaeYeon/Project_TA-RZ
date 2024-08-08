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
    [SerializeField] BehaviorTree Bt;
    [Header("적 타입")]
    [SerializeField] MonsterType Type;

    void Start()
    {
        var monsterhp = Bt.GetVariable("Hp");
        Debug.Log(monsterhp);
        //Bt의 변수값을 가져올 수 있음. 굳
    }

   
}
