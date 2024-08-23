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
    public override TaskStatus OnUpdate()
    {
        var targetTransform = Monster.Value.Player.transform.position;
        Monster.Value.StartAtk();        
        return TaskStatus.Success;
    }
}
