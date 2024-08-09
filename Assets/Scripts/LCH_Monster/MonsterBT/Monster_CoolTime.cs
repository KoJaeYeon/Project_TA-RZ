using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[TaskCategory("Monster/General")]
public class Monster_CoolTime : Conditional
{
    [SerializeField] SharedFloat CoolTime;
    [SerializeField] SharedFloat LastAtkTime;
    public override TaskStatus OnUpdate()
    {
        float nowTime = Time.time;
        if (nowTime - LastAtkTime.Value >= CoolTime.Value)
        {
            return TaskStatus.Success;  // 쿨타임이 끝났으므로 성공 반환
        }
        else
        {
            return TaskStatus.Failure;  // 쿨타임이 아직 끝나지 않았으므로 실패 반환
        }
    }
}
