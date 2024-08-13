using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_CheckHp : Conditional
{
    [SerializeField] SharedMonster Monster;
    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.Mon_Common_Stat_Hp <= 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
