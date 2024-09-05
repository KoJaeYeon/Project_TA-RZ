using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum MonsterList //순서 변경X
{
    _meleeAttackmonster,
    _meleeExplosionmonster,
    _chargeMonster,
    _longRangeMonster,
    _eliteMosnter
}

public enum ItemList //순서 변경X
{
    _resourceA,
    _resourceB, 
    _resourceC,
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
    [SerializeField] private GameObject _eliteMonster;
   

    [Header("Item")]
    [SerializeField] private int _itemCount;
    [SerializeField] private GameObject[] _items;

    private Dictionary<MonsterList, GameObject> _monsterDictionary = new Dictionary<MonsterList, GameObject>();
    private Dictionary<ItemList, GameObject> _itemDictionary = new Dictionary<ItemList, GameObject>();
    private Dictionary<ItemList, string> _itemNameDictionary = new Dictionary<ItemList, string>();

    private void Awake()
    {
        CreateMonster();
        CreateItem();
    }

    private void CreateMonster()
    {
        var monsters = new (MonsterList, GameObject)[]
        {
            (MonsterList._meleeAttackmonster, _meleeAttackmonster),
            (MonsterList._meleeExplosionmonster, _meleeExplosionmonster),
            (MonsterList._chargeMonster, _chargeMonster),
            (MonsterList._longRangeMonster, _longRangeMonster),
            (MonsterList._eliteMosnter, _eliteMonster)
        };

        AddMonster(monsters, _monsterCount, _monsterDictionary);
    }

    private void AddMonster((MonsterList, GameObject)[] monsters, int count,
     Dictionary<MonsterList, GameObject> monsterDictionary = null)
    {
        foreach (var (monsterType, monsterObject) in monsters)
        {
            _poolManager.CreatePool(monsterObject, count);
            monsterDictionary?.Add(monsterType, monsterObject);
        }
    }

    private void CreateItem()
    {
        var items = new (ItemList, GameObject)[]
        {
            (ItemList._resourceA, _items[0]),
            (ItemList._resourceB, _items[1]),
            (ItemList._resourceC, _items[2]),
        };

        AddItem(items, _itemCount, _itemDictionary);
    }

    private void AddItem((ItemList, GameObject)[] items, int count,
        Dictionary<ItemList, GameObject> itemDictionary = null)
    {
        foreach(var (itemType, itemObject) in items)
        {
            _poolManager.CreatePool(itemObject, count);
            itemDictionary?.Add(itemType, itemObject);
            _itemNameDictionary.Add(itemType, itemObject.name);
        }
    }

    public GameObject GetMonster(MonsterList monsterType)
    {
        if(_monsterDictionary.TryGetValue(monsterType, out GameObject monsterPrefab))
        {
            return _poolManager.DequeueObject(monsterPrefab);
        }
        else
        {
            _poolManager.CreatePool(monsterPrefab, _monsterCount);

            return _poolManager.DequeueObject(monsterPrefab);
        }
    }

    public GameObject GetItem(ItemList itemType)
    {
        if(_itemDictionary.TryGetValue(itemType, out GameObject itemPrefab))
        {
            return _poolManager.DequeueObject(itemPrefab);
        }
        else
        {
            _poolManager.CreatePool(itemPrefab, _itemCount);

            return _poolManager.DequeueObject(itemPrefab);
        }
    }

    public string GetItemName(ItemList itemType)
    {
        if(_itemNameDictionary.TryGetValue(itemType, out string itemName))
        {
            string name = itemName;

            return name;
        }
        else
        {
            Debug.Log("아이템 이름이 존재하지 않습니다.");
            return null;
        }
    }
}
