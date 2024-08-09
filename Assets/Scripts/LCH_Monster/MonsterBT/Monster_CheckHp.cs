using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_CheckHp : Conditional
{
    [SerializeField] SharedInt Hp;
    public override TaskStatus OnUpdate()
    {
        if (Hp.Value <= 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
