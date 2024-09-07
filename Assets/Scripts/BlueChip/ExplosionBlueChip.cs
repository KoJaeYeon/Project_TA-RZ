using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlueChip : BlueChip
{
    private event Action<float> _explosionAction;

    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _blueChipSystem = blueChipSystem;
        _data = data;
        _currentPower = _data.Att_damage;
        _targetLayer = LayerMask.GetMask("Monster");
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

        StartExplosion(position, _currentPower);
    }

    private void StartExplosion(Vector3 position, float currentPassivePower)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _data.Chip_AttackArea, _targetLayer);

        foreach(var target in  colliders)
        {
            IStatusEffect statusEffect = target.GetComponent<IStatusEffect>(); 

            if(statusEffect != null)
            {
                _explosionAction += (float passivePower) => statusEffect.Explosion(passivePower);
            }
        }

        _explosionAction?.Invoke(currentPassivePower);

        _explosionAction = null;
    }

    public override void SetEffectObject(GameObject effectObject)
    {
        
    }
}
