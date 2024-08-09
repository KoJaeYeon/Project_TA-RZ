using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_Track : Action
{
    [SerializeField] SharedTransform TargetTransform;
    [SerializeField] SharedFloat MoveSpeed;
    public override TaskStatus OnUpdate()
    {
        if(TargetTransform == null)
        {
            return TaskStatus.Failure;
        }

        Vector3 direction = (TargetTransform.Value.position - Owner.transform.position).normalized;

        if (TargetTransform != null)
        {
            Owner.transform.position += direction * MoveSpeed.Value * Time.deltaTime;
            return TaskStatus.Success;
        } 
        else return TaskStatus.Failure;
    }
}
