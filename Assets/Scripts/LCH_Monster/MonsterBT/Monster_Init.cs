using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
[TaskCategory("Monster/General")]
public class Monster_Init : Action
{
    //[SerializeField] SharedMonster_A monster_A;
    [SerializeField] SharedTransform _transform;
    [SerializeField] SharedMonster monster;
    [SerializeField] SharedNavmesh nav;
    [SerializeField] SharedFloat waitTime;
    [SerializeField] SharedCustomCollider collider;
    [SerializeField] SharedAnimator animator;
    public override void OnAwake()
    {
        monster.Value = GetComponent<Monster>();
        _transform.Value = monster.Value.Player.transform;
        nav.Value = GetComponent<NavMeshAgent>();
        waitTime.Value = monster.Value.Mon_Knockback_Time;
        //monster_A.Value = GetComponent<Monster_A>();
        collider.Value = Owner.GetComponentInChildren<BoxCollider>();
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
