using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Monster/A")]
public class Monster_A_OnAtk : Action
{
    [SerializeField] SharedMonster_A Monster;
    Animator animator;

    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }
    public override void OnStart()
    {
        animator.Play("Atk");
        Monster.Value.StartAtk();
    }
    public override TaskStatus OnUpdate()
    {        
        Monster.Value.StartAtk();
        return TaskStatus.Success;
    }
}
