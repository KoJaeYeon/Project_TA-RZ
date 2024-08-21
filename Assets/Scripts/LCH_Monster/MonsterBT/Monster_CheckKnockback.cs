using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_CheckKnockback : Conditional
{
    [SerializeField] SharedMonster Monster;

    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.isKnockBack == true)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
