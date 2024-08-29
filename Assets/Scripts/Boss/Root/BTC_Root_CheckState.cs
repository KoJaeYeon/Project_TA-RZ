using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[TaskCategory("Root")]
public class BTC_Root_CheckState : Conditional
{
    [SerializeField] private RootContoller _owner;

    public override void OnAwake()
    {
        _owner = GetComponent<RootContoller>();
    }

    public override TaskStatus OnUpdate()
    {
        if (_owner.rootState == RootState.Hide || _owner.rootState == RootState.Die) return TaskStatus.Success;

        return TaskStatus.Failure;
    }
}
