using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

[TaskCategory("Monster/Anim")]
public class Monster_Anim_CustomSetBool : Action
{
    [SerializeField] SharedAnimator anim;
    [SerializeField] SharedBool BoolValue;
    // Start is called before the first frame update

    public override TaskStatus OnUpdate()
    {
        if (anim == null)
        {
            anim.Value = Owner.GetComponentInChildren<Animator>();
            anim.Value.SetBool("IsTrack", BoolValue.Value);
            return TaskStatus.Success;
        }
            anim.Value.SetBool("IsTrack", BoolValue.Value);
            return TaskStatus.Success;
    }
}
