using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlueChip : BlueChip
{
    private event Action<float, float, float> _action;

    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _blueChipSystem = blueChipSystem;
        _data = data;
        _monsterLayer = LayerMask.GetMask("Monster");
        _currentPower = _data.Att_damage;
    }

    public override void ResetSystem()
    {
        
    }

    public override void LevelUpBlueChip()
    {
        _currentLevel++;

        if(_currentLevel <= 2)
        {
            _currentPower += _data.Att_Damage_Lvup;
        }
    }

    public override void UseBlueChip(Vector3 position, AttackType currentAttackType)
    {
        if(currentAttackType is AttackType.fourthAttack)
        {
            return;
        }

        StartPoison(position, _currentPower);
    }

    private void StartPoison(Vector3 position, float currentPassivePower)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _data.Chip_AttackArea, _monsterLayer);

        foreach(var target in  colliders)
        {
            IStatusEffect statusEffect = target.GetComponent<IStatusEffect>();

            if (statusEffect != null)
            {
                _action += (float passivePower, float maxTime, float intervalTime) => statusEffect.Poison(currentPassivePower, maxTime, intervalTime);
            }
        }

        _action?.Invoke(currentPassivePower, _data.Chip_Lifetime, _data.Interval_time);

        _action = null;
    }
}
