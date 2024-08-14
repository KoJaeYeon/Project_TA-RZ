using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : PlayerStaus
{
    public PlayerDeath(Player player) : base(player) { }

    private WaitForSeconds Pc_Failui_Time = new WaitForSeconds(2f);

    public override void StateEnter()
    {
        _animator.SetTrigger(_death);

        _animator.SetBool(_deathCheck, true);

        _player.StartCoroutine(DeathUI());

        _player.IsPlayerAlive = false;
    }

    public override void StateUpdate()
    {
        //만약 부활이 생긴다면 여기에 확인하는 코드 추가.
    }

    public override void StateExit()
    {
        //부활 시 코드 추가.
        //_animator.SetBool(_deathCheck, false);
    }

    private IEnumerator DeathUI()
    {
        yield return Pc_Failui_Time;

        Debug.Log("사망 UI");
    }

}
