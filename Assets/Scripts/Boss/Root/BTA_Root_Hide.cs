using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Root")]
public class BTA_Root_Hide : Action
{
    [SerializeField] private RootContoller _owner;
    private float _range = 10f;

    public override void OnAwake()
    {
        _owner = GetComponent<RootContoller>();
    }

    public override TaskStatus OnUpdate()
    {
        if (_owner.transform.position.y > -_range)
            _owner.transform.Translate(Vector3.down * Time.deltaTime);

        return TaskStatus.Success;
    }
}
