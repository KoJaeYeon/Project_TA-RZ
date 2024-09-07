using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlueChip : BlueChip
{
    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _blueChipSystem = blueChipSystem;

        _data = data;

        _targetLayer = LayerMask.GetMask("Monster");

        _currentPower = _data.Att_damage;
    }
    public override void SetEffectObject(GameObject effectObject)
    {
        _effectObject = effectObject;

        _poolManager.CreatePool(_effectObject);
    }

    public override void ResetSystem()
    {
        _currentLevel = 0;
        _currentPower = 0;
        _poolManager.AllDestroyObject(_effectObject);
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

        GameObject poisonObject = _poolManager.DequeueObject(_effectObject);

        PoisonObject objectComponent = poisonObject.GetComponent<PoisonObject>();

        objectComponent.SetObjectData(_data.Chip_Lifetime, _data.Chip_AttackArea
            , _currentPower, _data.Interval_time, _targetLayer);

        poisonObject.transform.position = position;

        objectComponent.StartPoison();
    }

    
}
