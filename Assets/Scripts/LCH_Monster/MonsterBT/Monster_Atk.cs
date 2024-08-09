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
    [SerializeField] SharedFloat LastAtkTime;
    public override TaskStatus OnUpdate()
    {
        

        LastAtkTime.Value = Time.time;
        Debug.Log(LastAtkTime.Value);
        return TaskStatus.Success;
    }

  
}
