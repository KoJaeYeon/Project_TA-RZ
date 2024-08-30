using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
[TaskCategory("Monster/D")]
public class Monster_D_Init : Action
{
    [SerializeField] SharedMonster monster;
    [SerializeField] SharedMonster_D monsterD;
    [SerializeField] SharedTransform _transform;
    [SerializeField] SharedNavmesh nav;
    [SerializeField] SharedFloat waitTime;
    [SerializeField] SharedCustomCollider collider;
    [SerializeField] SharedAnimator animator;
    public override void OnAwake()
    {
        monster.Value = GetComponent<Monster>();
        monsterD.Value = GetComponent <Monster_D>();
        nav.Value = GetComponent<NavMeshAgent>();
        _transform.Value = monsterD.Value.Player.transform;
        waitTime.Value = monsterD.Value.Mon_Knockback_Time;
        animator.Value = GetComponent<Animator>();
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
