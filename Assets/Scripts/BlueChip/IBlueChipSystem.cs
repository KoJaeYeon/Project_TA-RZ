using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlueChipSystem
{
    public void UseBlueChip(Vector3 position, float currentPassivePower, AttackType currentAttackType);
    public void LevelUpBlueChip();
    public void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data);
}
