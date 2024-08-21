using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_OnAtk : Action
{
    [SerializeField] SharedMonster monster;
    [SerializeField] SharedCustomCollider collider;
    // Start is called before the first frame update
    Animator anim;
    AnimatorStateInfo animinfo;
    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        anim.Play("Atk");
        collider.Value.enabled = true;
    }
    public override TaskStatus OnUpdate()
    {
        if (monster != null)
        {
            var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Atk") == true)
            {
                animinfo = anim.GetCurrentAnimatorStateInfo(0);
                if (animinfo.normalizedTime < 0.95f)
                {

                    return TaskStatus.Running;
                }

                else
                {
                    collider.Value.enabled = false;
                    return TaskStatus.Success;
                }

            }
            else if (stateInfo.IsName("get hit from front") == true)
            {
                return TaskStatus.Failure;
            }
            else
            {
                return TaskStatus.Running;
            }
        }

        return TaskStatus.Failure;
    }
}
