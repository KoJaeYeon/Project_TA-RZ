using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_CheckKnockback : Conditional
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] float knockbackForce = 0.5f;


    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.isKnockBack==true)
        {
            Monster.Value.ApplyKnockback(Monster.Value.Player.transform.position, knockbackForce);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

   
}
