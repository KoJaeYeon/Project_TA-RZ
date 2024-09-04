using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MonsterD")]
public class MonsterD_OnAtk : Action
{
    [SerializeField] SharedMonster_D Monster;

    public override void OnStart()
    {
        Monster.Value.IsFirstAtk= true;
        Monster.Value.OnAtk();
    }
    public override TaskStatus OnUpdate()
    {
        Debug.Log(Monster.Value.isDashing);
        if(Monster.Value.isDashing == false)
        {
            return TaskStatus.Success;
        }        
         
        return TaskStatus.Running;
    }

}
