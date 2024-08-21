using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_OnNav : Action
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] SharedNavmesh Nav;
    public override TaskStatus OnUpdate()
    {
        Monster.Value.isKnockBack = false;
        Nav.Value.enabled = true;
        //Nav.Value.isStopped = true;
        //Nav.Value.velocity = Vector3.zero;
        //Nav.Value.SetDestination(Owner.transform.position);
        return TaskStatus.Success;
    }
}