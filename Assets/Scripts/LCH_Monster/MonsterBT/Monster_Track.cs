using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[TaskCategory("Monster/General")]
public class Monster_Track : Action
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] SharedNavmesh Nav;
    public override TaskStatus OnUpdate()
    {
        if(Monster.Value.Player.transform == null)
        {
            return TaskStatus.Failure;
        }
     

        if (Monster.Value.Player.transform != null)
        {
            Nav.Value.isStopped = false;  // NavMeshAgent 다시 움직이기
            Nav.Value.speed = Monster.Value.Mon_Common_MovementSpeed;
            Nav.Value.acceleration = Monster.Value.Mon_Common_MovementSpeed * 3.5f / 8;
            Nav.Value.SetDestination(Monster.Value.Player.transform.position);
            return TaskStatus.Success;
        }
        else
        {
           
        }
            return TaskStatus.Failure;
    }
}
