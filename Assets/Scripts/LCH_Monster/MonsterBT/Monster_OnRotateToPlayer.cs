using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_OnRotateToPlayer : Action
{
    [SerializeField] SharedMonster monster;
    [SerializeField] TaskStatus returnStatus = TaskStatus.Success;

    public override TaskStatus OnUpdate()
    {
        if (monster != null)
        {
            Owner.transform.LookAt(monster.Value.Player.transform);
            Vector3 rot = Owner.transform.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            Owner.transform.eulerAngles = rot;
            return returnStatus;
        }

        return TaskStatus.Failure;
    }
}
