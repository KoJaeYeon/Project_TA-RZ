using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlueChipSystem : MonoBehaviour
{
    [Inject]
    private DataManager _dataManager;
    protected PC_BlueChip _data;

    public Action _blueChipSystem;

    #region Data
    protected float _attackDamage;
    protected float _attackLevelUp;
    protected float _attackRange;
    protected float _intervalTime;
    protected float _lifeTime;
    protected List<float> _valueList;
    #endregion

    protected IEnumerator LoadData(string id)
    {
        yield return new WaitWhile(() =>
        {
            Debug.Log($"{id}의 블루칩 데이터를 가져오지 못했습니다.");
            return _dataManager.GetData(id) == null;
        });

        var data = _dataManager.GetData(id) as PC_BlueChip;

        _data = data;

        _attackDamage = _data.Att_damage;
        _attackLevelUp = _data.Att_Damage_Lvup;
        _attackRange = _data.Chip_AttackArea;
        _intervalTime = _data.Interval_time;
        _lifeTime = _data.Chip_Lifetime;
        _valueList = new List<float>(_data.ValueList);
    }

    protected virtual void ExecuteBlueChip() { }

    protected virtual void LevelUpBlueChip() { }
}
