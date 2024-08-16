using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor;
using BehaviorDesigner.Runtime;

[TaskCategory("Monster/General")]
public class MonsterStun : Action
{
    [SerializeField] SharedMonster Monster;
    public override TaskStatus OnUpdate()
    {
        Monster.Value.StartCoroutine(Monster.Value.WaitForStun());
        return TaskStatus.Success;  
    }
}
