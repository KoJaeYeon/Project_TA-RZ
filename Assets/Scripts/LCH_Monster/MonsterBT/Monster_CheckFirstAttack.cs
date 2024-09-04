using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_CheckFirstAttack : Conditional
{
    [SerializeField] SharedMonster monster;

    public override TaskStatus OnUpdate()
    {
        


        return TaskStatus.Success;
    }
}
