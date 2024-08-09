using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_CoolTime : Conditional
{
    [SerializeField] SharedFloat CoolTime;

    public override TaskStatus OnUpdate()
    {
        float nowTime = Time.time;

        return base.OnUpdate();
    }
}
