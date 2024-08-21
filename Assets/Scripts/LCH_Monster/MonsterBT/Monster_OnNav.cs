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
        if (Time.time < Monster.Value.targetKnockbackTime)
        {
            return TaskStatus.Running;
        }

        Monster.Value.KnockbackEnd();
        Nav.Value.enabled = true;
        return TaskStatus.Success;
    }
}