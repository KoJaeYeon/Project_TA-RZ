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
    [SerializeField] SharedMonster Monster;
    [SerializeField] SharedInt CoolTime;
    public override TaskStatus OnUpdate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if(Monster.Value.Player.transform != null)
        {
            StartCoroutine(WaitNextAtk());
            animator.SetTrigger("Atk");
            Debug.Log("공갹");
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
        
        IEnumerator WaitNextAtk()
        {

            yield return new WaitForSeconds(CoolTime.Value);
        }

    }

  
}
