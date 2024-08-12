using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[TaskCategory("Monster/General")]
public class Monster_Track : Action
{
    [SerializeField] SharedTransform TargetTransform;
    [SerializeField] SharedFloat MoveSpeed;
    [SerializeField] NavMeshAgent Nav;
    [SerializeField] SharedFloat DetectArea;
    public override TaskStatus OnUpdate()
    {
        if(TargetTransform == null)
        {
            return TaskStatus.Failure;
        }
        if (Nav == null)
        {
            Nav = GetComponent<NavMeshAgent>();
        }

        if (TargetTransform != null)
        {
            Nav.SetDestination(TargetTransform.Value.position);
            return TaskStatus.Success;
        } 
        else return TaskStatus.Failure;
    }
}
