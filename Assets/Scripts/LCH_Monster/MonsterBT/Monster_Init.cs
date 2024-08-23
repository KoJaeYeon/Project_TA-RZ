using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
[TaskCategory("Monster/General")]
public class Monster_Init : Action
{
    [SerializeField] SharedTransform _transform;
    [SerializeField] SharedMonster monster;
    [SerializeField] SharedNavmesh nav;
    [SerializeField] SharedFloat waitTime;
    [SerializeField] SharedCustomCollider collider;
    public override void OnAwake()
    {
        monster.Value = GetComponent<Monster>();
        _transform.Value = monster.Value.Player.transform;
        nav.Value = GetComponent<NavMeshAgent>();
        waitTime.Value = monster.Value.Mon_Knockback_Time;
        

    }
    public override TaskStatus OnUpdate()
    {
       
        return TaskStatus.Success;
    }
}
