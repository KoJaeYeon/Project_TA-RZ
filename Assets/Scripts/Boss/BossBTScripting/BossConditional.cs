using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskCategory("Boss")]
public class BossConditional : Conditional
{
    protected SharedBossController _boss;
    public BossController _owner;
    protected Rigidbody _rb;
    protected Animator _anim;

    public override void OnAwake()
    {
        _boss = GetComponent<BossController>();
        _owner = GetComponent<BossController>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }
}
