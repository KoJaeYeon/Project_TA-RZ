using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_Atk : Action
{
    [SerializeField] Animator animator;
    [SerializeField] SharedFloat AtkSpeed;
    [SerializeField] SharedTransform TargetTransform;
    public override TaskStatus OnUpdate()
    {
        if(TargetTransform!= null)
        {
            Debug.Log("공갹");
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
        
    }

  
}
