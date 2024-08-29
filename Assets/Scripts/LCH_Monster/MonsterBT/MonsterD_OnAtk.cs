using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MonsterD")]
public class MonsterD_OnAtk : Action
{
    [SerializeField] SharedMonster_D Monster;

    public override TaskStatus OnUpdate()
    {
        Monster.Value.OnAtk(Monster.Value.Player.transform);
         
        return TaskStatus.Success;
    }

}
