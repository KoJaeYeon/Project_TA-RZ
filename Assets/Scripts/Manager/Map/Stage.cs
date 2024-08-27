using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Zenject;

[System.Serializable]
public enum GameLevel
{
    Beginning,
    Middle,
    Final
}

public struct TestData
{
    public int _maxResourceCount { get; }
    public float _resourceA_Ratio { get; }
    public float _resourceB_Ratio { get; }
    public float _resourceC_Ratio { get; }

    public TestData(int maxResourceCount, float resourceA_Ratio, float resourceB_Ratio, float resourceC_Ratio)
    {
        _maxResourceCount = maxResourceCount;
        _resourceA_Ratio = resourceA_Ratio;
        _resourceB_Ratio = resourceB_Ratio;
        _resourceC_Ratio = resourceC_Ratio;
    }
}

public class Stage : MonoBehaviour
{
    [System.Serializable] 
    public class Partition //각 구역을 나타내는 클래스.
    {
        public Transform _centerPosition; //중앙 포지션
        public float _mapSizeX; //맵 사이즈 X
        public float _mapSizeZ; //맵 사이즈 Z

        [HideInInspector]
        public int _maxResourceCount; //해당 구역의 전체 아이템 개수.
        [HideInInspector]
        public float _resourceA_Ratio; //A 아이템 비율
        [HideInInspector]
        public float _resourceB_Ration; //B 아이템 비율
        [HideInInspector]
        public float _resourceC_Ration; //C 아이템 비율
        [HideInInspector]
        public int[] _resourceCountArray; //A,B,C 아이템 비율에 따라 생성되어야 하는 각 아이템의 수를 저장하는 배열.
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
    [SerializeField] private List<Partition> _partitions; //각 구역 리스트.

    [Header("MonsterSpwanCount")]
    [SerializeField] private int _monsterSpawncount;

    [Header("PortalObject")]
    [SerializeField] private GameObject _portal;

    private StageType _currentStage;
    private TestData[] _testDataStruct;

    private StageObject _object;
    private List<Partition> _selectMonsterArea;
    private List<Partition> _selectItemArea;
    private List<GameObject> _spawnMonsters;
    private List<GameObject> _spawnItems;

    private void Awake()
    {
        _mapManager.SetStage(this);

        _object = gameObject.GetComponent<StageObject>();

        SetTestMapData(_level);
    }

    private void Start()
    {
        _currentStage = StageType.Normal;

        SpawnObject();
    }

   
    private void SpawnObject()
    {
        SetArea();
        SpawnMonster();
        SpawnItem();
    }

    //임시 데이터
    private void SetTestMapData(GameLevel level)
    {
        switch (level)
        {
            case GameLevel.Beginning:
                _testDataStruct = new TestData[2];
                _testDataStruct[0] = new TestData(20, 33f, 33f, 34f);
                _testDataStruct[1] = new TestData(30, 33f, 33f, 34f);
                break;
            case GameLevel.Middle:
                _testDataStruct = new TestData[6];
                _testDataStruct[0] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[1] = new TestData(25, 33f, 33f, 34f);
                _testDataStruct[2] = new TestData(25, 33f, 33f, 34f);
                _testDataStruct[3] = new TestData(25, 33f, 33f, 34f);
                _testDataStruct[4] = new TestData(25, 33f, 33f, 34f);
                _testDataStruct[5] = new TestData(25, 33f, 33f, 34f);
                break;
            case GameLevel.Final:
                _testDataStruct = new TestData[9];
                _testDataStruct[0] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[1] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[2] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[3] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[4] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[5] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[6] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[7] = new TestData(30, 33f, 33f, 34f);
                _testDataStruct[8] = new TestData(30, 33f, 33f, 34f);
                break;
        }
    }

    private void SetPartitionsItemArea()
    {
        _selectItemArea = new List<Partition>();

        for (int i = 0; i < _partitions.Count; i++)
        {
            if( i < _testDataStruct.Length)
            {
                var testData = _testDataStruct[i];
                var partition = _partitions[i];

                partition._maxResourceCount = testData._maxResourceCount;
                partition._resourceA_Ratio = testData._resourceA_Ratio / 100f;
                partition._resourceB_Ration = testData._resourceB_Ratio / 100f;
                partition._resourceC_Ration = testData._resourceC_Ratio / 100f;

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
        }
    }
  
    private void SetPartitionsMonsterArea(GameLevel level)
    {
        _selectMonsterArea = new List<Partition>();

        int selectPartition = 0;

        switch (level)
        {
            case GameLevel.Beginning:
                selectPartition = 1;
                break;
            case GameLevel.Middle:
                selectPartition = 3;
                break;
            case GameLevel.Final:
                selectPartition = 5;
                break;
        }

        //파티션 리스트의 수를 초과하지 않도록 최소값을 보장.
        selectPartition = Mathf.Min(selectPartition, _partitions.Count);

        if(level == GameLevel.Beginning)
        {
            _selectMonsterArea.Add(_partitions[selectPartition]);
        }
        else
        {
            int[] randomArray = Enumerable.Range(1, _partitions.Count - 1).ToArray();

            System.Random random = new System.Random();

            int randomArrayCount = randomArray.Length;

            while(randomArrayCount > 1)
            {
                randomArrayCount--;

                int randomIndex = random.Next(1, randomArrayCount + 1);

                int value = randomArray[randomIndex];

                randomArray[randomIndex] = randomArray[randomArrayCount];

                randomArray[randomArrayCount] = value;
            }

            for(int i = 0; i < selectPartition; i++)
            {
                int index = randomArray[i];

                _selectMonsterArea.Add(_partitions[index]);
            }

        }
    }

    private void SetArea()
    {
        SetPartitionsItemArea();
        SetPartitionsMonsterArea(_level);
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

                    switch (i)
                    {
                        case (int)ItemList._resourceA:
                            GameObject itemA = _object.GetItem(ItemList._resourceA);
                            itemA.transform.position = spawnPosition;
                            _spawnItems.Add(itemA);
                            break;
                        case (int)ItemList._resourceB:
                            GameObject itemB = _object.GetItem(ItemList._resourceB);
                            itemB.transform.position = spawnPosition;
                            _spawnItems.Add(itemB);
                            break;
                        case (int)ItemList._resourceC:
                            GameObject itemC = _object.GetItem(ItemList._resourceC);
                            itemC.transform.position = spawnPosition;
                            _spawnItems.Add(itemC);
                            break;
                    }
                }
            }
        }

        Debug.Log(_spawnItems.Count);
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

    private Vector3 GetRandomSpawnPosition(Transform centerPosition, float mapSizeX, float mapSizeZ)
    {
        float randomPosX = UnityEngine.Random.Range(-mapSizeX / 2, mapSizeX / 2);
        float randomPosZ = UnityEngine.Random.Range(-mapSizeZ / 2, mapSizeZ / 2);

        Vector3 randomPosition = new Vector3(centerPosition.position.x + randomPosX,
            centerPosition.position.y + 1.5f, centerPosition.position.z + randomPosZ);

        return randomPosition;
    }

}

