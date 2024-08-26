using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum MonsterList
{
    _meleeAttackmonster,
    _meleeExplosionmonster,
    _chargeMonster,
    _longRangeMonster
}

public class StageObject : MonoBehaviour
{
  
    [Inject]
    private PoolManager _poolManager;

    [Header("Monster")]
    [SerializeField] private int _monsterCount;
    [SerializeField] private GameObject _meleeAttackmonster;
    [SerializeField] private GameObject _meleeExplosionmonster;
    [SerializeField] private GameObject _chargeMonster;
    [SerializeField] private GameObject _longRangeMonster;

    [Header("Item")]
    [SerializeField] private int _itemCount;
    [SerializeField] private GameObject[] _items;

    private Dictionary<MonsterList, GameObject> _monsterDictionary = new Dictionary<MonsterList, GameObject>();

    private void Awake()
    {
        CreateMonster();
        CreateItem();
    }

    private void CreateMonster()
    {
        _poolManager.CreatePool(_meleeAttackmonster, _monsterCount);
        _poolManager.CreatePool(_meleeExplosionmonster, _monsterCount);
        _poolManager.CreatePool(_chargeMonster, _monsterCount);
        _poolManager.CreatePool(_longRangeMonster, _monsterCount);

        AddMonster();
    }

    private void CreateItem()
    {
        for(int i = 0; i < _items.Length; i++)
        {
            _poolManager.CreatePool(_items[i], _itemCount);
        }
    }

    private void AddMonster()
    {
        _monsterDictionary.Add(MonsterList._meleeAttackmonster, _meleeAttackmonster);
        _monsterDictionary.Add(MonsterList._meleeExplosionmonster, _meleeExplosionmonster);
        _monsterDictionary.Add(MonsterList._chargeMonster, _chargeMonster);
        _monsterDictionary.Add(MonsterList._longRangeMonster, _longRangeMonster);
    }

    public GameObject GetMonster(MonsterList monsterType)
    {
        if(_monsterDictionary.TryGetValue(monsterType, out GameObject monsterPrefab))
        {
            return _poolManager.DequeueObject(monsterPrefab);
        }
        else
        {
            return null;
        }
    }

    public GameObject GetItem()
    {
        int randomIndex = Random.Range(0, _items.Length);

        GameObject item = _poolManager.DequeueObject(_items[randomIndex]);

        return item;
    }

}
