using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MonsterD")]
public class Monster_D_WaitDash : Action
{
    [SerializeField] SharedMonster_D Monster;


    public override void OnStart()
    {
        Monster.Value.CheckBeforeDash(Monster.Value.Player.transform);
    }


    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.IsDrawDash == false)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }


}
