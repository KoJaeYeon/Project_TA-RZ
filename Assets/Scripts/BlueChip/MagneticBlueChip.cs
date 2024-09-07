using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBlueChip : BlueChip
{
    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _targetLayer = LayerMask.GetMask("Item");
    }

    public override void ResetSystem()
    {
        
    }

    public override void LevelUpBlueChip()
    {
        
    }

    public override void UseBlueChip(Vector3 position, AttackType currentAttackType)
    {
        if(currentAttackType is AttackType.fourthAttack)
        {
            
        }
    }

    
}
