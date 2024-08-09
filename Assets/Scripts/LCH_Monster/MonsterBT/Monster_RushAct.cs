using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Monster/Specific")]
public class Monster_RushAct : Action
{
    [SerializeField] SharedTransform TargetTransform;  // 목표 위치
    [SerializeField] SharedFloat RushSpeed;  // 돌진 속도

    public override TaskStatus OnUpdate()
    {
        // 현재 적의 위치
        Vector3 currentPosition = transform.position;

        // 목표 위치
        Vector3 targetPosition = TargetTransform.Value.position;


        float step = RushSpeed.Value * Time.deltaTime;

        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, step);

        transform.position = newPosition;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            return TaskStatus.Success;  
        }

        return TaskStatus.Running;  
    }
}
