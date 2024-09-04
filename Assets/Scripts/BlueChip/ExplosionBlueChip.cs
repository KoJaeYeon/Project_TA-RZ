using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlueChip : BlueChipSystem
{
    public ExplosionBlueChip()
    {
        StartCoroutine(LoadData("G202"));
    }

    protected override void ExecuteBlueChip()
    {
        
    }

    protected override void LevelUpBlueChip()
    {
        _attackDamage += _attackLevelUp;
    }
}
