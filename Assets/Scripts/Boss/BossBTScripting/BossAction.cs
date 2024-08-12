using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Boss")]
public class BossAction : Action
{
    public BossController _owner;
    protected Rigidbody _rb;
    protected Animator _anim;

    public override void OnAwake()
    {
        _owner = GetComponent<BossController>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }
}
