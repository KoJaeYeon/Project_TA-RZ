using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MagneticBlueChip : BlueChip
{
    private List<Rigidbody> _itemList = new List<Rigidbody>();
    private List<Rigidbody> _removeList = new List<Rigidbody>();

    private float _currentRadius;
    private float _speed;
    private float _force;

    private bool _pull;

    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _blueChipSystem = blueChipSystem;
        _data = data;
        _currentRadius = 6f;
        _force = 10f;
        _speed = 5f;
        _targetLayer = LayerMask.GetMask("Item");
    }
    public override void SetEffectObject(GameObject effectObject)
    {
        _effectObject = effectObject;
    }

    public override void ResetSystem()
    {
        _currentLevel = 0;
        _currentRadius = 0f;
    }

    public override void LevelUpBlueChip()
    {
        _currentLevel++;

        if(_currentLevel <= 2)
        {
            _currentRadius = (_data.Chip_AttackArea + _data.Att_Damage_Lvup) / 2;
        }
    }

    public override void UseBlueChip(Transform transform, AttackType currentAttackType, bool isStart = true)
    {
        bool fourthAttack = currentAttackType is AttackType.fourthAttack;

        if (!fourthAttack)
        {
            return;
        }

        if (isStart)
        {
            _pull = true;
        }
        else
        {
            _pull = false;

            return;
        }

        _blueChipSystem.StartCoroutine(PullItem(transform));
    }

    private IEnumerator PullItem(Transform transform) 
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, _currentRadius, _targetLayer);

        foreach (var item in colliders)
        {
            Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

            if (itemRigidbody != null)
            {
                _itemList.Add(itemRigidbody);
            }
        }

        while(_pull)
        {
            foreach (var item in _itemList)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);

                if(distance > 0.7f)
                {
                    Vector3 drainDirection = transform.position - item.transform.position;

                    float currentItemspeed = item.velocity.magnitude;

                    if (currentItemspeed > _speed)
                    {
                        item.velocity = item.velocity.normalized * _speed;
                    }

                    item.AddForce(drainDirection * Time.deltaTime * _force, ForceMode.Impulse);

                    if (!item.gameObject.activeSelf)
                    {
                        _removeList.Add(item);
                    }
                }
            } 

            if(_removeList.Count > 0) 
            {
                foreach (var item in _removeList)
                {
                    _itemList.Remove(item);
                }

                _removeList.Clear();
            }
            
            yield return null;
        }

        _itemList.Clear();

        _removeList.Clear();
    }
}
