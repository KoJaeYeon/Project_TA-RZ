using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Editor.BuildingBlocks;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_CheckDamaged : Conditional
{
    [SerializeField] SharedMonster Monster;
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.isDamaged == true)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;

    }
}
