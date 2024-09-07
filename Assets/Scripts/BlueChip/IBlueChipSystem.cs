using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlueChipSystem
{
    public void UseBlueChip(Vector3 position, AttackType currentAttackType);
    public void LevelUpBlueChip();
    public void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data);
    public void SetEffectObject(GameObject effectObject);
    public void ResetSystem();
}
