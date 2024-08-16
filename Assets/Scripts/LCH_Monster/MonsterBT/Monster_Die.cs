using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_Die : Action
{
    public override TaskStatus OnUpdate()
    {
        //BehaviorTree bt = GetComponent<BehaviorTree>();
        //bt.enabled = false; 
        Owner.gameObject.SetActive(false);
        
        return TaskStatus.Success;
    }
}
