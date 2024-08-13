using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.AI;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_Stop : Action
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] NavMeshAgent Nav;

    public override TaskStatus OnUpdate()
    {
        if (Nav == null)
        {
            Nav = GetComponent<NavMeshAgent>();
        }

        // 몬스터와 목표 사이의 거리 계산
        float distanceToTarget = Vector3.Distance(Monster.Value.Player.transform.position, Owner.transform.position);

        // 목표가 공격 범위 안에 있을 때 NavMeshAgent 멈추기
        if (distanceToTarget <= Monster.Value.Mon_Common_Range)
        {
            Nav.isStopped = true;  // NavMeshAgent 멈추기
            Nav.velocity = Vector3.zero;
            Debug.Log("멈춤");
            return TaskStatus.Success;  
        }

        else if(distanceToTarget >= Monster.Value.Mon_Common_Range)
        {
            Debug.Log("안멈춤");
            Nav.isStopped = false;  // NavMeshAgent 다시 움직이기

            return TaskStatus.Failure;  
        }
        return TaskStatus.Success;
    }
}
