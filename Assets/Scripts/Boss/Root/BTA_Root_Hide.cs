using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Root")]
public class BTA_Root_Hide : Action
{
    [SerializeField] private RootContoller _owner;
    private float _range = 10f;

    private Vector3 _startPos;
    Vector3 direction;

    private bool _isDash = false;

    public override void OnAwake()
    {
        _owner = GetComponent<RootContoller>();
    }

    public override void OnStart()
    {
        _startPos = transform.position;
        _isDash = true;
    }

    public override TaskStatus OnUpdate()
    {
        _owner.transform.Translate(Vector3.down * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, _startPos);

        if (distance >= _range)
        {
            _isDash = false;
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
