using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("MonsterC")]
public class Monster_C_CheckState : Conditional
{
    [SerializeField] SharedMonster_C Monster;
    [SerializeField] TaskStatus ReturnType = TaskStatus.Running;

    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.IsKnockBack == true)
        {
            return TaskStatus.Failure;
        }
        else if (Monster.Value.IsDamaged == true)
        {
            return TaskStatus.Failure;
        }
        //else if (Monster.Value.IsAttack==true)
        else
        {
            return ReturnType;
        }
    }
}
