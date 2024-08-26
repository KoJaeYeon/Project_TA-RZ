using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[System.Serializable]
public enum GameLevel
{
    Beginning,
    Middle,
    Final
}

public class Stage : MonoBehaviour
{
    [System.Serializable]
    public class Partition
    {
        public Transform _centerPosition;
        public float _mapSizeX;
        public float _mapSizeZ;

        [HideInInspector]
        public int _maxResourceCount;
        [HideInInspector]
        public float _resourceA_Ratio;
        [HideInInspector]
        public float _resourceB_Ration;
        [HideInInspector]
        public float _resourceC_Ration;
        [HideInInspector]
        public int[] _resourceCountArray;
    }

    #region InJect
    [Inject]
    private MapManager _mapManager;
    [Inject]
    private DataManager _dataManager;
    #endregion

    [Header("GameLevel")]
    [SerializeField] private GameLevel _level;

    [Header("PartitionList")]
    [SerializeField] private List<Partition> _partitions;

    [Header("MonsterSpwanCount")]
    [SerializeField] private int _monsterSpawncount;

    [Header("ItemSpawnCount")]
    [SerializeField] private int _itemSpawncount = 100;

    [Header("PortalObject")]
    [SerializeField] private GameObject _portal;

    private StageType _currentStage;

    private StageObject _object;
    private List<Partition> _selectMonsterArea;
    private List<Partition> _selectItemArea;
    private List<GameObject> _spawnMonsters;
    private List<GameObject> _spawnItems;

    private int _totalResource;
    private float _resourceA_Ratio;
    private float _resourceB_Ratio;
    private float _resourceC_Ratio;

    private void Awake()
    {
        _mapManager.SetStage(this);
        _object = gameObject.GetComponent<StageObject>();
        _totalResource = 100;
    }

    private void Start()
    {
        _currentStage = StageType.Normal;

        SpawnObject();
    }

   
    private void SpawnObject()
    {
        SelectRandomArea();
        SpawnMonster();
        SpawnItem();
    }

    private void SelectRandomArea()
    {
        _selectItemArea = new List<Partition>();

        foreach (var partition in _partitions)
        {
            partition._maxResourceCount = 20;
            partition._resourceA_Ratio = 0.33f;
            partition._resourceB_Ration = 0.33f;
            partition._resourceC_Ration = 0.34f;

            int resourceA = Mathf.FloorToInt(partition._resourceA_Ratio * partition._maxResourceCount);
            int resourceB = Mathf.FloorToInt(partition._resourceB_Ration * partition._maxResourceCount);
            int resourceC = Mathf.FloorToInt(partition._resourceC_Ration * partition._maxResourceCount);

            int total = resourceA + resourceB + resourceC;

            int excess = partition._maxResourceCount - total;

            resourceC += excess;

            partition._resourceCountArray = new int[3];

            partition._resourceCountArray[0] = resourceA;
            partition._resourceCountArray[1] = resourceB;
            partition._resourceCountArray[2] = resourceC;

            _selectItemArea.Add(partition);
        }

        _selectMonsterArea = new List<Partition>();

        int index = 0;

        switch (_level)
        {
            case GameLevel.Beginning:
                break;
            case GameLevel.Middle:
                index = 3;
                break;
            case GameLevel.Final:
                index = 5;
                break;
        }

        if (index == 0)
        {
            _selectMonsterArea.Add(_partitions[1]);
        }
        else
        {
            for(int i = 0; i < index; i++)
            {
                int randomArea = Random.Range(1,_partitions.Count);

                _selectMonsterArea.Add(_partitions[randomArea]);

                _partitions.RemoveAt(randomArea);
            }
        }

        

    }

    private void SpawnMonster()
    {
        _spawnMonsters = new List<GameObject>();

        foreach(var partition in _selectMonsterArea)
        {
            for(int i = 0; i < _monsterSpawncount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition(partition._centerPosition, partition._mapSizeX, partition._mapSizeZ);

                if(spawnPosition != null)
                {
                    InstantiateMonster(_currentStage, spawnPosition);
                }
            }
        }
    }

    private void SpawnItem()
    {
        _spawnItems = new List<GameObject>();   

        foreach(var partition in _selectItemArea)
        {
            for(int i = 0; i < partition._resourceCountArray.Length; i++)
            {
                for(int k = 0; k < partition._resourceCountArray[i]; k++)
                {
                    Vector3 spawnPosition = GetRandomSpawnPosition(partition._centerPosition, partition._mapSizeX, partition._mapSizeZ);

                    GameObject item = _object.GetItem();

                    item.transform.position = spawnPosition;

                    _spawnItems.Add(item);
                }
            }
        }
    }

    private void InstantiateMonster(StageType stageType, Vector3 spawnPosition)
    {
        switch(stageType)
        {
            case StageType.Normal:
                SpawnNormal(spawnPosition);
                break;
            case StageType.Quantity:
                SpawnQuantity(spawnPosition);
                break;
            case StageType.Elite:
                SpawnElite(spawnPosition);
                break;
        }
    }

    private void SpawnNormal(Vector3 spawnPosition)
    {
        //몬스터 조합 데이터를 가져와서 조합에 해당하는 몬스터를 생성.
        GameObject testMonster = _object.GetMonster(MonsterList._meleeAttackmonster);

        testMonster.transform.position = spawnPosition;

        _spawnMonsters.Add(testMonster);
    }

    private void SpawnQuantity(Vector3 spawnPosition)
    {

    }

    private void SpawnElite(Vector3 spawnPosition)
    {

    }

    public void MonsterDeath(GameObject monster)
    {

    }

    private Vector3 GetRandomSpawnPosition(Transform centerPosition, float mapSizeX, float mapSizeZ)
    {
        float randomPosX = Random.Range(-mapSizeX / 2, mapSizeX / 2);
        float randomPosZ = Random.Range(-mapSizeZ / 2, mapSizeZ / 2);

        Vector3 randomPosition = new Vector3(centerPosition.position.x + randomPosX,
            centerPosition.position.y + 1.5f, centerPosition.position.z + randomPosZ);

        return randomPosition;
    }

}
