using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("MonsterD")]
public class Monster_D_WaitDash : Action
{
    [SerializeField] SharedMonster_D Monster;
    float duration = 1f;
    float elapsedTime = 0f;


    public override void OnStart()
    {
        Monster.Value.CheckBeforeDash(Monster.Value.Player.transform);
        Monster.Value.Anim.SetTrigger("beforeAtk");
        Monster.Value.IsDrawDash = true;
        elapsedTime = 0f;
    }


    public override TaskStatus OnUpdate()
    {        
        elapsedTime += Time.deltaTime;

        if (elapsedTime < duration)
        {
            Monster.Value.dashUi.DashGauge.fillAmount = Mathf.Lerp(0, 1, elapsedTime / duration);
        }
        else
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        Monster.Value.dashUi.DashGauge.fillAmount = 0;
    }


}
