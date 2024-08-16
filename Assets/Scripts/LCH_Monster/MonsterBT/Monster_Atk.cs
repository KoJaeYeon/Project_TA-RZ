using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster/General")]
public class Monster_Atk : Action
{
    [SerializeField] Animator animator;
    [SerializeField] SharedMonster Monster;
    [SerializeField] NavMeshAgent Nav;
    public override TaskStatus OnUpdate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (Nav == null)
        {
            Nav = GetComponent<NavMeshAgent>();
        }
        if (Monster.Value.Player.transform != null)
        {
            animator.SetTrigger("Atk");
            Nav.isStopped = true;
            Debug.Log("공갹");
            return TaskStatus.Success;
        }
        Nav.isStopped = false;
        Nav.velocity = Vector3.zero;
        return TaskStatus.Failure;



    }

  
}
