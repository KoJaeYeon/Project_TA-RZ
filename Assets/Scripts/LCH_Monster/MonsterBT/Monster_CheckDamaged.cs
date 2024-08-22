using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_CheckDamaged : Conditional
{
    [SerializeField] SharedMonster Monster;
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        if (Monster.Value.IsDamaged == true)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;

    }
}
