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
    [SerializeField] SharedNavmesh Nav;

    public override TaskStatus OnUpdate()
    {
        //Nav.Value.isStopped = true;
        //// Nav.velocity = Vector3.zero;
        //Nav.Value.velocity = Vector3.zero;
        //Nav.Value.SetDestination(Owner.transform.position);
        //Debug.Log("please");
        return TaskStatus.Success;

    }
}
