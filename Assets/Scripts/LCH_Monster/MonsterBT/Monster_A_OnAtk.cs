using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[TaskCategory("Monster/A")]
public class Monster_A_OnAtk : Action
{
    [SerializeField] SharedMonster_A monster;
    [SerializeField] SharedAnimator anim;
    AnimatorStateInfo animinfo;
    
    public override void OnStart()
    {
        anim.Value.Play("Atk");
        monster.Value.StartAtk();
        monster.Value.IsFirstAtk = true;
    }
    public override TaskStatus OnUpdate()
    {
        if (monster.Value != null)
        {
            var stateInfo = anim.Value.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Atk") == true)
            {
                animinfo = anim.Value.GetCurrentAnimatorStateInfo(0);

                if (animinfo.normalizedTime < 0.32f)
                {

                }
                else if (animinfo.normalizedTime < 0.55f)
                {

                }
                else if (animinfo.normalizedTime < 0.80f)
                {
                }
                else
                {
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
}
[TaskCategory("Monster/C")]
public class Monster_C_OnAtk : Action
{
    [SerializeField] SharedMonster_C Monster;
    [SerializeField] SharedAnimator animator;
    AnimatorStateInfo animinfo;
    public override void OnStart()
    {
        animator.Value.Play("Atk");
        Monster.Value.StartAtk();
        Monster.Value.IsFirstAtk = true;

    }
    public override TaskStatus OnUpdate()
    {
        if (Monster.Value != null)
        {
            if (Monster.Value.IsAttack == true)
            {
                return TaskStatus.Running;
            }
            
        }

        return TaskStatus.Failure;
    }
}

