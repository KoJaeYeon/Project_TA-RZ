using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_CheckDamaged : Conditional
{
    [SerializeField] SharedMonster Monster;

    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.IsKnockBack == true)
        {
            return TaskStatus.Failure;
        }
        else if (Monster.Value.IsDamaged == true)
        {
            return TaskStatus.Running;
        }
        else 
        {
            return TaskStatus.Failure;
        }

    }
}
