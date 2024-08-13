using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

[TaskCategory("Monster/Anim")]
public class Monster_CustomSetBool : Action
{
    [SerializeField] Animator anim;
    [SerializeField] SharedBool BoolValue;
    // Start is called before the first frame update

    public override TaskStatus OnUpdate()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            anim.SetBool("IsTrack", BoolValue.Value);
            return TaskStatus.Running;
        }
            anim.SetBool("IsTrack", BoolValue.Value);
            return TaskStatus.Success;
    }
}
