using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Monster/General")]
public class Monster_CheckTarget : Conditional
{
    [SerializeField] SharedTransform TargetTransform;
    [SerializeField] SharedFloat AtkDistance;
    public override TaskStatus OnUpdate()
    {
        float distance = Vector3.Distance(TargetTransform.Value.position, Owner.transform.position);
        if(distance>= AtkDistance.Value)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
