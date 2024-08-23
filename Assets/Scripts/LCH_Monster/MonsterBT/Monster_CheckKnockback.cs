using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_CheckKnockback : Conditional
{
    [SerializeField] SharedMonster Monster;

    public override TaskStatus OnUpdate()
    {
        return Monster.Value.IsKnockBack == true ? TaskStatus.Success : TaskStatus.Failure;
    }
}
