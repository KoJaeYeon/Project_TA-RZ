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
    [SerializeField] SharedFloat MoveSpeed;
    [SerializeField] NavMeshAgent Nav;
    [SerializeField] SharedFloat DetectArea;
    public override TaskStatus OnUpdate()
    {
        if(Monster.Value.Player.transform == null)
        {
            return TaskStatus.Failure;
        }
        if (Nav == null)
        {
            Nav = GetComponent<NavMeshAgent>();
        }

        if (Monster.Value.Player.transform != null)
        {
            Nav.SetDestination(Monster.Value.Player.transform.position);
            return TaskStatus.Success;
        } 
        else return TaskStatus.Failure;
    }
}
