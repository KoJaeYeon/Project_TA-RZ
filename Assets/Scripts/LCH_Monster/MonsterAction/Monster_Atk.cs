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
    public override TaskStatus OnUpdate()
    {
        StartCoroutine(WaitForNextAtk());
        return TaskStatus.Success;
    }

    IEnumerator WaitForNextAtk()
    {
        Debug.Log("ㅎㅇ");
        yield return new WaitForSeconds(AtkSpeed.Value);
    }
}
