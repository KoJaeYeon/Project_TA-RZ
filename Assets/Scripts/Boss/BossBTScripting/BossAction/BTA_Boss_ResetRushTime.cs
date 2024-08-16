using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Boss/Rush")]
public class BTA_Boss_ResetRushTime : BossAction
{
    [SerializeField] private SharedFloat _defaultRushTime;
    [SerializeField] private SharedFloat _rushTime;

    public override TaskStatus OnUpdate()
    {
        _rushTime.Value = _defaultRushTime.Value;

        return TaskStatus.Success;
    }
}
