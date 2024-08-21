using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

public class BTC_Boss_CheckCoolDown : BossConditional
{
    [SerializeField, Tooltip("0 = 기믹, 1 = 뿌리공격, 2 = 내려치기, 3 = 폭발, 4 = 돌진, 5 = 휘두르기")] private SharedInt _patterns;
    public override TaskStatus OnUpdate()
    {
        switch (_patterns.Value)
        {
            case 0:
                if (_owner.CheckCoolDown(_boss.Value.isCoolGimmick))
                {
                    return TaskStatus.Success;
                }
                break;

            case 1:
                if (_owner.CheckCoolDown(_boss.Value.isCoolRootAttack))
                {
                    return TaskStatus.Success;
                }
                break;

            case 2:
                if (_owner.CheckCoolDown(_boss.Value.isCoolSmash))
                {
                    return TaskStatus.Success;
                }
                break;

            case 3:
                if (_owner.CheckCoolDown(_boss.Value.isCoolExplosion))
                {
                    return TaskStatus.Success;
                }
                break;

            case 4:
                if (_owner.CheckCoolDown(_boss.Value.isCoolRush))
                {
                    return TaskStatus.Success;
                }
                break;

            case 5:
                if (_owner.CheckCoolDown(_boss.Value.isCoolSwing))
                {
                    return TaskStatus.Success;
                } 
                break;
        }

        return TaskStatus.Failure;
    }
}
