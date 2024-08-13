using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Monster/General")]
public class Monster_CheckTarget : Conditional
{
    [SerializeField] SharedMonster Monster;
    
    public override TaskStatus OnUpdate()
    {
        float distance = Vector3.Distance(Monster.Value.Player.transform.position, Owner.transform.position);
        if (distance >= Monster.Value.Mon_Common_Range)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Success;
    }
}
