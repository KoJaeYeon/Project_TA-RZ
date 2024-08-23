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
    }
    public override TaskStatus OnUpdate()
    {
        if (monster != null)
        {
            var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Atk") == true)
            {
                animinfo = anim.GetCurrentAnimatorStateInfo(0);

                if(animinfo.normalizedTime < 0.32f)
                {

                }
                else if (animinfo.normalizedTime < 0.55f)
                {
                    collider.Value.enabled = true;

                }
                else if (animinfo.normalizedTime < 0.80f)
                {
                    collider.Value.enabled = false;
                }
                else
                {
                    collider.Value.enabled = false;
                    return TaskStatus.Success;
                }
                return TaskStatus.Running;

            }
            else if (stateInfo.IsName("get hit from front") == true)
            {
                return TaskStatus.Failure;
            }
            else if (stateInfo.IsName("Idle") == true)
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

    public override void OnEnd()
    {
        collider.Value.enabled = false;
    }
}
