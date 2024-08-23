using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_CheckState : Conditional
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] TaskStatus ReturnType = TaskStatus.Running;

    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.IsKnockBack==true)
        {
            return TaskStatus.Failure;
        }
        else if(Monster.Value.IsDamaged==true)
        {
            return TaskStatus.Failure;
        }
        else
        {
            return ReturnType;
        }        
    }   
}
