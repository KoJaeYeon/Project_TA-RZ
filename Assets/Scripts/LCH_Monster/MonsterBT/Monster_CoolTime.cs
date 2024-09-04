using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class Monster_CoolTime : Conditional
{
    [SerializeField] SharedMonster Monster;
    

    public override TaskStatus OnUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - Monster.Value.LastAttackTime >= Monster.Value.Mon_Common_CoolTime)
        {
            //공격쿨타임 
            Monster.Value.LastAttackTime = Time.time + 1000f;
            return TaskStatus.Success;
        }
        else if (Monster.Value.IsFirstAtk == false)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure; 
        }
    }
}
