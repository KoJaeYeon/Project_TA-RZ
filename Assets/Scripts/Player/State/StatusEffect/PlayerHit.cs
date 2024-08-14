using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : PlayerStaus
{
    public PlayerHit(Player player) : base(player) { }

    public static float Pc_Stiff_Time { get; set; } = 0.4f;

    

    public override void StateEnter()
    {
        //_animator.SetLayerWeight(1, 1f);

        _animator.SetTrigger(_comboFail);

        _animator.SetTrigger(_paralysis);

        _coroutine = _player.StartCoroutine(Paralysis(Pc_Stiff_Time));

        _animator.SetFloat(_speed, 0f);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void StateExit()
    {
        _animator.ResetTrigger(_comboFail);

        //_animator.SetLayerWeight(1, 0f);

        base.StateExit();
    }

    private IEnumerator Paralysis(float stiffTime)
    {

        _rigidBody.velocity = Vector3.zero;

        yield return new WaitForSeconds(stiffTime);

        _rigidBody.velocity = Vector3.zero;

        _rigidBody.angularVelocity = Vector3.zero;

        _state.ChangeState(State.Idle);
    }
}
