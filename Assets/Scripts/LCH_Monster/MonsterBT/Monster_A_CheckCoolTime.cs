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
            Monster.Value.LastAttackTime = Time.time + 1000f;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

[TaskCategory("MonsterC")]

public class Monster_C_CheckCoolTime : Conditional
{
    [SerializeField] SharedMonster_C Monster;

    public override TaskStatus OnUpdate()
    {
        float currentTime = Time.time;
        if(currentTime - Monster.Value.LastAttackTime >= Monster.Value.Mon_Common_CoolTime)
        {
            Monster.Value.LastAttackTime = Time.time + 1000f;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

[TaskCategory("MonsterD")]

public class Monster_D_CheckCoolTime : Conditional
{
    [SerializeField] SharedMonster_D Monster;

    public override TaskStatus OnUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - Monster.Value.LastAttackTime >= Monster.Value.Mon_Common_CoolTime)
        {
            Monster.Value.LastAttackTime = Time.time + 1000f;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }


    }
}
