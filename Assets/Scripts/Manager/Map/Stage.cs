using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Zenject;

[System.Serializable]
public enum GameLevel //게임 레벨
{
    Beginning,
    Middle,
    Final,
    Boss
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
        public float _resourceB_Ratio; //B 아이템 비율
        [HideInInspector]
        public float _resourceC_Ratio; //C 아이템 비율
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

    [Header("PortalObject")]
    [SerializeField] private GameObject _portal;

    [Header("GameUI")]
    [SerializeField] private GameObject _gameUI;

    private StageType _currentStage;

    private StageObject _object;
    private List<Partition> _selectMonsterArea;
    private List<Partition> _selectItemArea;
    private HashSet<GameObject> _spawnMonsters;
    private List<GameObject> _spawnItems;

    #region StageData
    private Dictionary<StageType, List<Map_Monster_Mix>> _monsterDataDictionary;
    private Map_Resource _itemData;
    //private int _totalResource;
    private bool _itemDataReady = false;
    private bool _monsterDataReady = false;

    #endregion
    private void Awake()
    {
        _portal.SetActive(false);

        _mapManager.SetStage(this);

        _object = gameObject.GetComponent<StageObject>();

        StartCoroutine(SetItemData($"R{1+(int)_level}01"));
        StartCoroutine(SetMonsterData());
    }

    private void Start()
    {
        _currentStage = _mapManager.GetStageType();

        StartCoroutine(SpawnObject());
    }

    //오브젝트를 스폰하는 코루틴
    private IEnumerator SpawnObject() 
    {
        yield return new WaitUntil(() => (_itemDataReady && _monsterDataReady));

        SetArea();
        SpawnMonster();
        SpawnItem();
    }

    private IEnumerator SetItemData(string idStr)
    {
        while (true)
        {
            var itemData = _dataManager.GetData(idStr) as Map_Resource;

            if(itemData == null)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log("현재 스테이지의 아이템 데이터를 읽어오지 못했습니다.");
            }
            else
            {
                Debug.Log("현재 스테이지의 아이템 데이터를 읽어왔습니다.");
                _itemData = itemData;
                _itemDataReady = true;
                yield break;
            }
        }
    }

    private IEnumerator SetMonsterData()
    {
        _monsterDataDictionary = new Dictionary<StageType, List<Map_Monster_Mix>>();

        while (true)
        {
            var monsterData = _dataManager.GetData("L301") as Map_Monster_Mix;

            if(monsterData == null)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log("몬스터 데이터를 읽어오지 못했습니다.");
            }
            else
            {
                for(int i = 0; i < 9; i++)
                {
                    monsterData = _dataManager.GetData($"L30{i + 1}") as Map_Monster_Mix;

                    if(i < 3)
                    {
                        AddMonsterData(StageType.Quantity, monsterData);
                    }
                    else if(i < 6)
                    {
                        AddMonsterData(StageType.Normal, monsterData);
                    }
                    else
                    {
                        AddMonsterData(StageType.Elite, monsterData);
                    }
                }

                Debug.Log("몬스터 데이터를 성공적으로 읽어왔습니다.");
                _monsterDataReady = true;
                yield break;
            }
        }

    }

    //딕셔너리에 몬스터 조합 정보를 넣는 메서드
    private void AddMonsterData(StageType stageType, Map_Monster_Mix data) 
    {
        if (!_monsterDataDictionary.ContainsKey(stageType))
        {
            _monsterDataDictionary[stageType] = new List<Map_Monster_Mix>();
        }

        _monsterDataDictionary[stageType].Add(data);
    }

    //랜덤한 조합 정보를 가져오는 메서드
    private Map_Monster_Mix GetRandomData(StageType currentStage)
    {
        List<Map_Monster_Mix> dataList = _monsterDataDictionary[currentStage];

        int randomIndex = UnityEngine.Random.Range(0, dataList.Count);

        return dataList[randomIndex];
    }

    //각 구역에 스폰할 아이템을 결정하는 메서드
    private void SetPartitionsItemArea()
    {
        _selectItemArea = new List<Partition>();

        for (int i = 0; i < _partitions.Count; i++)
        {
            if( i < _itemData.Map_Panel_Resources_Ratio.Count)
            {
                var panelData = _itemData;
                var partition = _partitions[i];

                partition._maxResourceCount = panelData.Map_Panel_Resources_Ratio[i];
                partition._resourceA_Ratio = panelData.Map_Resources_type_Ratio[0] / 100f;
                partition._resourceB_Ratio = panelData.Map_Resources_type_Ratio[1] / 100f;
                partition._resourceC_Ratio = panelData.Map_Resources_type_Ratio[2] / 100f;

                int resourceA = Mathf.FloorToInt(partition._resourceA_Ratio * partition._maxResourceCount);
                int resourceB = Mathf.FloorToInt(partition._resourceB_Ratio * partition._maxResourceCount);
                int resourceC = Mathf.FloorToInt(partition._resourceC_Ratio * partition._maxResourceCount);

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
  
    //몬스터가 스폰할 구역을 결정하는 메서드
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

    //몬스터 스폰 메서드
    private void SpawnMonster()
    {
        _spawnMonsters = new HashSet<GameObject>();

        foreach(var partition in _selectMonsterArea)
        {
            Map_Monster_Mix randomData = GetRandomData(_currentStage);

            for(int i = 0; i < randomData.Mon_Monster.Count; i++)
            {
                for(int k = 0; k < randomData.Mon_Monster[i]; k++)
                {
                    Vector3 spawnPosition = GetRandomSpawnPosition(partition._centerPosition, partition._mapSizeX, partition._mapSizeZ);

                    if(spawnPosition != null)
                    {
                        GameObject monster = _object.GetMonster((MonsterList)i);

                        monster.transform.position = spawnPosition;

                        RegisterMonster(monster);
                    }
                }
            }
        }
    }

    //아이템 스폰 메서드
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

    //스폰된 몬스터를 해쉬셋에 등록하는 메서드
    private void RegisterMonster(GameObject monster)
    {
        Monster monsterComponent = monster.GetComponent<Monster>();

        monsterComponent.IsSpawn(this);

        _spawnMonsters.Add(monster);
    }

    //스폰된 몬스터에서 등록을 해제하는 메서드
    public void UnRegisterMonster(GameObject monster)
    {
        if (_spawnMonsters.Contains(monster))
        {
            _spawnMonsters.Remove(monster);

            if(_spawnMonsters.Count == 0)
            {
                _mapManager.RequestChangeProgressValue(0.33f);
                _portal.SetActive(true);
                _gameUI.SetActive(true);
            }
        }
    }

    //중앙 포지션을 기준으로 구역의 크기를 결정하고 몬스터나 아이템이 스폰할 좌표를 반환하는 메서드
    private Vector3 GetRandomSpawnPosition(Transform centerPosition, float mapSizeX, float mapSizeZ)
    {
        float randomPosX = UnityEngine.Random.Range(-mapSizeX / 2, mapSizeX / 2);
        float randomPosZ = UnityEngine.Random.Range(-mapSizeZ / 2, mapSizeZ / 2);

        Vector3 randomPosition = new Vector3(centerPosition.position.x + randomPosX,
            centerPosition.position.y + 1.5f, centerPosition.position.z + randomPosZ);

        return randomPosition;
    }

}

