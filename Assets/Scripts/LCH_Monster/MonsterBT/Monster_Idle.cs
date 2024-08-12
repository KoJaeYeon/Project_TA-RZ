using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Monster/General")]
public class Monster_Idle : Action
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] SharedFloat TrackDistance;

    public override TaskStatus OnUpdate()
    {
        Vector3 ownerPos = Owner.transform.position;
        var targetTrans = Monster.Value.Player.transform;
        Vector3 targetPos = targetTrans.position;

        float distance = Vector3.Distance(ownerPos, targetPos);
        if (distance >= TrackDistance.Value)
        {
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }
}
