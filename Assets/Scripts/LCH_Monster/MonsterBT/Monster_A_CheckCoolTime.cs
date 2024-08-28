using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;
[TaskCategory("MonsterA")]
public class Monster_A_CheckCoolTime : Conditional
{
    [SerializeField] SharedMonster_A Monster;

    public override TaskStatus OnUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - Monster.Value.LastAttackTime >= Monster.Value.Mon_Common_CoolTime)
        {
            //공격쿨타임 
            Monster.Value.LastAttackTime = Time.time + 10f;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}