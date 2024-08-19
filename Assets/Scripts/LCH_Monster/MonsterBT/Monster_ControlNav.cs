using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster/General")]
public class Monster_ControlNav : Action
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] NavMeshAgent Nav;
    public override TaskStatus OnUpdate()
    {
        Nav = GetComponent<NavMeshAgent>();
        Nav.isStopped = true;
        // Nav.velocity = Vector3.zero;
        Nav.velocity = Vector3.zero;
        Nav.SetDestination(Owner.transform.position);
        Debug.Log("please");
        return TaskStatus.Success;

    }
}

[TaskCategory("Monster/General")]
public class Monster_OnNav : Action
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] NavMeshAgent Nav;
    public override TaskStatus OnUpdate()
    {
        Monster.Value.isKnockBack = false;

        Nav = GetComponent<NavMeshAgent>();
        Nav.isStopped = true;
        Nav.velocity = Vector3.zero;
        Nav.SetDestination(Owner.transform.position);
        return TaskStatus.Success;
    }
}
