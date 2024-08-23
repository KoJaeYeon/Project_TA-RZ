using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWait : Action
{
    [SerializeField] SharedMonster Monster;


    public override TaskStatus OnUpdate()
    {


        return TaskStatus.Success;
    }
}
