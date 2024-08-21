using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Root")]
public class BTC_Boss_Root_CheckDistance : BossConditional
{
    [SerializeField] private SharedFloat _range;

    public override TaskStatus OnUpdate()
    {
        if (_owner == null)
        {
            Debug.LogWarning("보스가 없음");
            return TaskStatus.Failure;
        }

        if (_owner.CheckRootDistance(_range.Value)) return TaskStatus.Success;

        return TaskStatus.Failure;
    }
}
