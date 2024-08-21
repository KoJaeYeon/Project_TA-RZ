using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Monster/General")]
public class Monster_CheckKnockback : Conditional
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] SharedNavmesh Nav;

    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.isKnockBack==true)
        {
            Nav.Value.enabled = false;
            Monster.Value.ApplyKnockback(1, Monster.Value.Player.transform);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

   
}
