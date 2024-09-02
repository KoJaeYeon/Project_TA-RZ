using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
[TaskCategory("Monster/A")]
public class Monster_A_Init : Action
{
    [SerializeField] SharedMonster monster;
    [SerializeField] SharedMonster_A monster_A;
    [SerializeField] SharedTransform _transform;
    [SerializeField] SharedNavmesh nav;
    [SerializeField] SharedFloat waitTime;
    [SerializeField] SharedCustomCollider collider;
    [SerializeField] SharedAnimator animator;
    public override void OnAwake()
    {
        monster.Value = GetComponent<Monster>();
        monster_A.Value = GetComponent<Monster_A>();
        nav.Value = GetComponent<NavMeshAgent>();
        _transform.Value = monster_A.Value.Player.transform;
        waitTime.Value = monster_A.Value.Mon_Knockback_Time;
        
    }
    public override void OnStart()
    {
        animator.Value = Owner.GetComponentInChildren<Animator>();
    }
    public override TaskStatus OnUpdate()
    {

        return TaskStatus.Success;
    }
}
