using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class BlueChip : IBlueChipSystem
{
    #region InJect
    [Inject]
    protected PoolManager _poolManager;
    #endregion

    protected BlueChipSystem _blueChipSystem;
    protected PC_BlueChip _data;
    protected int _currentLevel;
    protected float _currentPower;
    protected LayerMask _targetLayer;

    public abstract void UseBlueChip(Vector3 position, AttackType currentAttackType);
    public abstract void LevelUpBlueChip();
    public abstract void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data);
    public abstract void ResetSystem();
}
