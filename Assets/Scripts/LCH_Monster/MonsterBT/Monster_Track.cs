using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[TaskCategory("Monster/General")]
public class Monster_Track : Action
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] NavMeshAgent Nav;

    Vector3 _targetPosition;
    Vector3 _ownerPosition;

    float _currentDistance;
    float _currentSpeed;

    public override TaskStatus OnUpdate()
    {
        TrackGetComponent();

        _targetPosition = Monster.Value.Player.transform.position;
        _ownerPosition = Owner.transform.position;

        _currentDistance = Vector3.Distance(_targetPosition, _ownerPosition);

        if(_currentDistance > Nav.stoppingDistance)
        {
            Nav.SetDestination(_targetPosition);

            return TaskStatus.Running;
        }

        return TaskStatus.Success;
        
        //if (Monster.Value.Player.transform == null)
        //{
        //    return TaskStatus.Failure;
        //}
        //if (Nav == null)
        //{
        //    Nav = GetComponent<NavMeshAgent>();
        //}

        

        //if (Monster.Value.Player.transform != null)
        //{

        //    Nav.speed = Monster.Value.Mon_Common_MovementSpeed;
        //    Nav.SetDestination(Monster.Value.Player.transform.position);
        //    return TaskStatus.Success;
        //}
        //else
        //{
           
        //}
        //    return TaskStatus.Failure;
    }

    private void TrackGetComponent()
    {
        if(Nav == null)
        {
            Nav = gameObject.GetComponent<NavMeshAgent>();
        }
    }
}
