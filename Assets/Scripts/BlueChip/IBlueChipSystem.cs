using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlueChipSystem
{
    public void UseBlueChip(Transform transform, AttackType currentAttackType, bool isStart = true);
    public void LevelUpBlueChip();
    public void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data);
    public void SetEffectObject(GameObject effectObject);
    public void ResetSystem();
}
