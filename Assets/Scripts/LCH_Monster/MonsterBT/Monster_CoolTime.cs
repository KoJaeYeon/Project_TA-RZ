using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine;

public class Monster_CoolTime : Conditional
{
    [SerializeField] SharedMonster Monster;
    [SerializeField] SharedFloat AtkDistance;
    [SerializeField] SharedFloat CoolTime;
    private float lastAttackTime = -Mathf.Infinity;

    public override TaskStatus OnUpdate()
    {
        float currentTime = Time.time;
        if (currentTime - lastAttackTime >= Monster.Value.Mon_Common_CoolTime)
        {
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
