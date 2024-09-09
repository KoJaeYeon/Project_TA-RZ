using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonExplosionRedChip : BlueChip
{
    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _blueChipSystem = blueChipSystem;

        _data = data;

        _currentPower = _data.Att_damage;

        _targetLayer = LayerMask.GetMask("Monster");
    }
    public override void SetEffectObject(GameObject effectObject)
    {
        _effectObject = effectObject;
    }

    public override void ResetSystem()
    {
        _poolManager.AllDestroyObject(_effectObject);
    }
    
    public override void LevelUpBlueChip()
    {
        return;
    }

    public override void UseBlueChip(Transform transform, AttackType currentAttackType, bool isStart = true)
    {
        if(currentAttackType is AttackType.fourthAttack)
        {
            return;
        }

        PoisonExplosion(transform);
    }

    private void PoisonExplosion(Transform transform)
    {
        GameObject poisonObject = _poolManager.DequeueObject(_effectObject);

        PoisonObject objectComponent = poisonObject.GetComponent<PoisonObject>();

        objectComponent.SetObjectData(_data.Chip_AttackArea, _currentPower, _targetLayer);

        Vector3 objectPosition = transform.position + transform.forward * 7f;

        poisonObject.transform.position = objectPosition;

        objectComponent.StartPoison(PoisonAttackType.Explosive);
    }
}
