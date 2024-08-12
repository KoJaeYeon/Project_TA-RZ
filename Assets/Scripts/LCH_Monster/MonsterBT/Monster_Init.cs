using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Zenject;
[TaskCategory("Monster/General")]
public class Monster_Init : Action
{
    [SerializeField] SharedTransform _transform;
    [SerializeField] SharedMonster monster;
    
    public override TaskStatus OnUpdate()
    {
        monster.Value = GetComponent<Monster>();
        _transform.Value = monster.Value.Player.transform;
        return TaskStatus.Success;
    }
}
