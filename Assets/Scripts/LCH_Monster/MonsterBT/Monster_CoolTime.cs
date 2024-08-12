using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_CoolTime : Conditional
{
    [SerializeField] SharedTransform TargetTransform;
    [SerializeField] SharedFloat AtkDistance;
    public override TaskStatus OnUpdate()
    {
        float distance = Vector3.Distance(TargetTransform.Value.position, Owner.transform.position);
        if (distance >= AtkDistance.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;  
        }
    }
    
}
