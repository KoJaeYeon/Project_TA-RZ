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
        if (Monster.Value.isKnockBack==true)
        {
            return TaskStatus.Failure;
        }
        else if(Monster.Value.isDamaged==true)
        {
            return TaskStatus.Failure;
        }
        else
        {
            return ReturnType;
        }        
    }   
}
