using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_OnAtk : Action
{
    [SerializeField] SharedMonster monster;
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
                if (animinfo.normalizedTime < 0.95f)
                {

                    return TaskStatus.Running;
                }

                else
                {
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
            //var animinfo = anim.GetCurrentAnimatorStateInfo(0);
            //if (animinfo.normalizedTime < 1)
            //{
            //    return TaskStatus.Running;
            //}
            //else
            //{
            //    return TaskStatus.Success;
            //}
        }

        return TaskStatus.Failure;
    }
}
