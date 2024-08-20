using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Monster/General")]
public class Monster_CheckTarget : Conditional
{
    [SerializeField] SharedMonster Monster;
    private Vector3 _targetPosition;
    private Vector3 _ownerPosition;
    private float _currentDistance;

    public override TaskStatus OnUpdate()
    {
        bool isTarget = CheckTarget();

        if (isTarget)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;

    
        //float distance = Vector3.Distance(Monster.Value.Player.transform.position, Owner.transform.position);

    
        //if (distance >= Monster.Value.Mon_Common_Range)
        //{
        //    return TaskStatus.Success;
        //}
        //return TaskStatus.Success;

    
    }

    private bool CheckTarget()
    {
        _targetPosition = Monster.Value.Player.transform.position;

        _ownerPosition = Owner.transform.position;

        _currentDistance = Vector3.Distance(_ownerPosition, _targetPosition);

        if(_currentDistance <= Monster.Value.Mon_Common_DetectArea)
        {
            return true;
        }

        return false;
    }
}
