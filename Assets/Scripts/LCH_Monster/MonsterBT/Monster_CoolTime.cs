using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_CoolTime : Conditional
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] SharedFloat AtkDistance;
    public override TaskStatus OnUpdate()
    {
        float distance = Vector3.Distance(Monster.Value.Player.transform.position, Owner.transform.position);
        if (distance >= AtkDistance.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;  
        }
    }
    
}
