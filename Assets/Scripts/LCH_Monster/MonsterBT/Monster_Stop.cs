using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.AI;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_Stop : Action
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] SharedNavmesh Nav;

    public override TaskStatus OnUpdate()
    {
        // 몬스터와 목표 사이의 거리 계산
        float distanceToTarget = Vector3.Distance(Monster.Value.Player.transform.position, Owner.transform.position);

        // 목표가 공격 범위 안에 있을 때 NavMeshAgent 멈추기
        if (distanceToTarget <= Monster.Value.Mon_Common_Range || Monster.Value.isCollsion == true)
        {
            Nav.Value.isStopped = true;  // NavMeshAgent 멈추기
            Nav.Value.velocity = Vector3.zero;
            Monster.Value.isCollsion = false;
            Debug.Log("멈춤");
            return TaskStatus.Success;  
        }
        else if(distanceToTarget >= Monster.Value.Mon_Common_Range)
        {
            Debug.Log("안멈춤");
            Nav.Value.SetDestination(Monster.Value.Player.transform.position);            

            return TaskStatus.Running;  
        }
        return TaskStatus.Failure;
    }
}
