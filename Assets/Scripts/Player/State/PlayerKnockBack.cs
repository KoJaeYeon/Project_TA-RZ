using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockBack : PlayerState
{
    public PlayerKnockBack(Player player) : base(player) { }
    
    public static Vector3 _knockBackPosition { get; set; }
    private float Pc_Knock_Back_Time = 1f;
    private float Pc_Knock_Back_Speed = 5f;

    public override void StateEnter()
    {
        _player.StartCoroutine(KnockBack());
        _animator.SetTrigger(_knockBack);
        _animator.speed = 2.6f / Pc_Knock_Back_Time;
        _animator.SetFloat("Speed", 0f);

        _player.transform.LookAt(_knockBackPosition);
        Vector3 rot = _player.transform.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        _player.transform.eulerAngles = rot;
    }

    public override void InputCheck() { }
    
    public override void StateExit()
    {
        _animator.speed = 1f;
    }

    private IEnumerator KnockBack()
    {
        _rigidBody.velocity = Vector3.zero;

        Vector3 knockBackDirection = _player.transform.position - _knockBackPosition;

        knockBackDirection.y = 0;

        knockBackDirection.Normalize();

        Vector3 knockBack = (knockBackDirection + Vector3.up) * Pc_Knock_Back_Speed;

        _rigidBody.AddForce(knockBack, ForceMode.Impulse);

        yield return new WaitForSeconds(Pc_Knock_Back_Time);

        _rigidBody.velocity = Vector3.zero;

        _rigidBody.angularVelocity = Vector3.zero;
        _state.ChangeState(State.Idle);
    }

}
