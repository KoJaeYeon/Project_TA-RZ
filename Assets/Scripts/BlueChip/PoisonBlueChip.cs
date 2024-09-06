using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlueChip : BlueChip
{
    private float _maxTime;

    private WaitForSeconds _intervalTime;

    private event Action _action;

    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _blueChipSystem = blueChipSystem;
        _data = data;
        _maxTime = _data.Chip_Lifetime;
        _intervalTime = new WaitForSeconds(_data.Interval_time);
        _monsterLayer = LayerMask.GetMask("Monster");
    }

    public override void LevelUpBlueChip()
    {
        
    }

    public override void UseBlueChip(Vector3 position, float currentPassivePower, AttackType currentAttackType)
    {
        if(currentAttackType is AttackType.fourthAttack)
        {
            return;
        }

        _blueChipSystem.StartCoroutine(Poison(position, currentPassivePower));
    }

    //private IEnumerator Poison(Vector3 position, float currentPassivePower)
    //{
    //    Collider[] colliders = Physics.OverlapSphere(position, _data.Chip_AttackArea, _monsterLayer);

    //    float timer = 0;

    //    //foreach(var monster in colliders)
    //    //{
    //    //    IHit hit = monster.GetComponent<IHit>();

    //    //    if(hit != null)
    //    //    {
    //    //        _action += hit.Hit;
    //    //    }
    //    //}

    //    //while(timer < _maxTime)
    //    //{
    //    //    _action.Invoke();
    //    //    yield return _intervalTime;
    //    //    timer += Time.deltaTime;
    //    //}

        
    //}

    
}
