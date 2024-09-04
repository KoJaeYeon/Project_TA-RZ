using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
[TaskCategory("Monster/C")]
public class Monster_C_Init : Action
{
    [SerializeField] SharedMonster monster;
    [SerializeField] SharedMonster_C monster_C;
    [SerializeField] SharedTransform _transform;
    [SerializeField] SharedNavmesh nav;
    [SerializeField] SharedFloat waitTime;
    [SerializeField] SharedCustomCollider collider;
    [SerializeField] SharedAnimator animator;
    public override void OnAwake()
    {
        monster.Value = GetComponent<Monster>();
        monster_C.Value = GetComponent<Monster_C>();
        nav.Value = GetComponent<NavMeshAgent>();
        _transform.Value = monster_C.Value.Player.transform;
        waitTime.Value = monster_C.Value.Mon_Knockback_Time;
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
