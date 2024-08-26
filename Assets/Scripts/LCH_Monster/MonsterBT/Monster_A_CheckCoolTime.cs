using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;
[TaskCategory("MonsterA")]
public class Monster_A_CheckCoolTime : Conditional
{
    [SerializeField] SharedMonster_A Monster;
    private float lastAttackTime = -Mathf.Infinity;

    public override TaskStatus OnUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - lastAttackTime >= Monster.Value.Mon_Common_CoolTime)
        {
            //공격쿨타임 
            lastAttackTime = currentTime;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
