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
        _currentPower = 0f;
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

    public override void UseBlueChip(Transform transform, AttackType currentAttackType, bool isStart = true)
    {
        if(currentAttackType is AttackType.fourthAttack)
        {
            return;
        }

        SpawnPoison(transform);
    }

    private void SpawnPoison(Transform transform)
    {
        GameObject poisonObject = _poolManager.DequeueObject(_effectObject);

        PoisonObject objectComponent = poisonObject.GetComponent<PoisonObject>();

        objectComponent.SetObjectData(_data.Chip_AttackArea, _currentPower * (1 +_player.PassiveAtk_Power/ 100), _targetLayer,
            _data.Chip_Lifetime, _data.Interval_time);

        Vector3 objectPosition = transform.position + transform.forward * 5f;

        poisonObject.transform.position = objectPosition;

        objectComponent.StartPoison(PoisonAttackType.Sustained);
    }
}
