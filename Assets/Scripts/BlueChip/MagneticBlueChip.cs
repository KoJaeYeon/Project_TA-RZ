using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBlueChip : BlueChip
{
    private List<Rigidbody> _itemList = new List<Rigidbody>();
    private List<Rigidbody> _removeList = new List<Rigidbody>();

    private float _currentRadius;
    private float _speed;
    private float _maxTime;
    private float _startTime;

    private bool _pull;

    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _blueChipSystem = blueChipSystem;
        _data = data;
        _currentRadius = _data.Chip_AttackArea / 2;
        _maxTime = 6f;        
    }
    public override void SetEffectObject(GameObject effectObject)
    {

    }

    public override void ResetSystem()
    {
        
    }

    public override void LevelUpBlueChip()
    {
        _currentLevel++;

        if(_currentLevel <= 2)
        {
            _currentRadius = (_data.Chip_AttackArea + _data.Att_Damage_Lvup) / 2;
        }
    }

    public override void UseBlueChip(Vector3 position, AttackType currentAttackType)
    {
        if(currentAttackType is AttackType.fourthAttack)
        {
            _blueChipSystem.StartCoroutine(PullItem(position));
        }
    }

    private IEnumerator PullItem(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _currentRadius, _targetLayer);

        foreach (var item in colliders)
        {
            Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

            if (itemRigidbody != null)
            {
                _itemList.Add(itemRigidbody);
            }
        }

        while(_itemList.Count > 0)
        {
            foreach (var item in _itemList)
            {
                Vector3 drainDirection = position - item.transform.position;
                item.AddForce(drainDirection * Time.deltaTime * _speed);
                float distance = Vector3.Distance(position, item.position);

                if (distance <= 1.5f)
                {
                    _removeList.Add(item);
                }
            }

            if(_removeList.Count > 0)
            {
                foreach (var item in _removeList)
                {
                    _itemList.Remove(item);
                }
            }
            
            yield return null;
        }
    }
}
