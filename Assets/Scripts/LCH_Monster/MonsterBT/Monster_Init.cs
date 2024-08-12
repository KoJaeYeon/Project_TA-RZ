using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Zenject;
[TaskCategory("Monster/General")]
public class Monster_Init : Action
{
    [SerializeField] SharedTransform targetTransform;
    [Inject] Player player;
    public override TaskStatus OnUpdate()
    {
        //targetTransform.Value = player.transform;

        return TaskStatus.Success;
    }
}
