using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Monster/General")]
public class Monster_CheckKnockback : Conditional
{
    [SerializeField] SharedMonster Monster;
    public float knockbackForce = 10000f;

    private bool _applyKnockback = false;

    public override TaskStatus OnUpdate()
    {
        if (_applyKnockback)
        {
            Vector3 knockbackDirection = (Monster.Value.transform.position - Vector3.forward).normalized;
            Monster.Value.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            _applyKnockback = false;  
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("넉백");
            _applyKnockback = true;  
        }
    }
}
