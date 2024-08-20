using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
[TaskCategory("Monster/General")]
public class Monster_Idle : Action
{

    [SerializeField] SharedMonster Monster;
    [SerializeField] NavMeshAgent Nav;

    private Vector3 _targetPosition;
    private Vector3 _ownerPosition;
    private float _currentDistance;

    public override TaskStatus OnUpdate()
    {
        IdleGetComponent();
        //Vector3 ownerPos = Owner.transform.position;
        //var targetTrans = Monster.Value.Player.transform;
        //Vector3 targetPos = targetTrans.position;

        _ownerPosition = Owner.transform.position;

        _targetPosition = Monster.Value.Player.transform.position;

        //float distance = Vector3.Distance(ownerPos, targetPos);
        _currentDistance = Vector3.Distance(_ownerPosition, _targetPosition);
        
        if(_currentDistance >= Monster.Value.Mon_Common_DetectArea)
        {
            return TaskStatus.Running;
        }

        return TaskStatus.Success;

        //if (Nav == null)
        //{
        //    Nav = GetComponent<NavMeshAgent>();
        //}
        //if (distance >= Monster.Value.Mon_Common_DetectArea)
        //{
        //    Nav.SetDestination(Owner.transform.position);
        //    return TaskStatus.Running;

        //}

    }

    private void IdleGetComponent()
    {
        if(Nav == null)
        {
            Nav = gameObject.GetComponent<NavMeshAgent>();
        }
    }
}
