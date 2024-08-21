using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class Monster_CoolTime : Conditional
{
    [SerializeField] SharedMonster Monster;
    private float lastAttackTime = -Mathf.Infinity;

    public override TaskStatus OnUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - lastAttackTime >= Monster.Value.Mon_Common_CoolTime)
        {
            //공격쿨타임 
            Debug.Log(Monster.Value.Mon_Common_CoolTime);
            lastAttackTime = currentTime;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure; 
        }
    }
}
