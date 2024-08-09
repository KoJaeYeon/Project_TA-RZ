using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_Atk : Action
{
    [SerializeField] Animator animator;
    [SerializeField] SharedFloat AtkSpeed;
    [SerializeField] SharedTransform TargetTransform;
    public override TaskStatus OnUpdate()
    {
        Vector3 ownerPos = Owner.transform.position;
        var targetTrans = TargetTransform.Value;
        Vector3 targetPos = targetTrans.position;
        float atkRange = Vector3.Distance(ownerPos,targetPos);

        
        StartCoroutine(WaitForNextAtk());
        return TaskStatus.Success;
    }

    IEnumerator WaitForNextAtk()
    {
        Debug.Log("ㅎㅇ");
        yield return new WaitForSeconds(AtkSpeed.Value);
    }
}
