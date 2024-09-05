using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum MapType //순서 변경X
{
    Lobby,
    Beginning,
    Middle,
    Final,
    Boss
}

public class MapManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private string[] _mapNameArray;

    private Dictionary<MapType, string> _mapNameDictionary;

    public float ProgressValue { get; set; }
    public float EliteChance { get; set; }

    private StageType _currentStageType;
    private string _mapName;
    private GameObject _currentMap;

    [Inject]
    public DiContainer _di;
    [Inject]
    private UIEvent _uiEvent;
    [Inject]
    private DataManager _dataManager;
    

    private void Awake()
    {
        InitializeMapManager();
    }

    private void Start()
    {
        StartCoroutine(LoadMap("Lobby"));
    }

    #region Initialize

    private void InitializeMapManager()
    {
        _uiEvent.RegisterChangeStage(this);

        _mapNameDictionary = new Dictionary<MapType, string>();

        if(_mapNameArray == null)
        {
            Debug.LogWarning("_mapNameArray가 존재하지 않습니다.");
            return;
        }

        var map = new (MapType, string)[]
        {
            (MapType.Lobby, _mapNameArray[0]),
            (MapType.Beginning, _mapNameArray[1]),
            (MapType.Middle, _mapNameArray[2]),
            (MapType.Final, _mapNameArray[3]),
            (MapType.Boss, _mapNameArray[4])
        };

        AddMap(map, _mapNameDictionary);

        StartCoroutine(LoadElite());
    }

    private void AddMap((MapType, string)[] map,
        Dictionary<MapType, string> mapNameDictionary = null)
    {
        foreach (var (mapType, mapObject) in map)
        {
            mapNameDictionary?.Add(mapType, mapObject);
        }
    }

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _currentMap.SetActive(false);

            _mapName = GetMap(MapType.Boss);

            StartCoroutine(LoadMap(_mapName));
        }
    }

    public void ChangeMap(StageType newStage)
    {
        _currentStageType = newStage;

        _currentMap.SetActive(false);

        if(ProgressValue < 0.33f)
        {
            _mapName = GetMap(MapType.Beginning);

            StartCoroutine(LoadMap(_mapName));
        }
        else if(ProgressValue < 0.66f)
        {
            _mapName = GetMap(MapType.Middle);

            StartCoroutine(LoadMap(_mapName));
        }
        else if(ProgressValue <= 0.99f)
        {
            _mapName = GetMap(MapType.Final);

            StartCoroutine(LoadMap(_mapName));
        }
        else
        {
            _mapName = GetMap(MapType.Boss);

            StartCoroutine(LoadMap(_mapName));
        }
    }

    public void LobbyMap()
    {
        _mapName = GetMap(MapType.Lobby);

        StartCoroutine(LoadMap(_mapName));
    }


    private string GetMap(MapType mapType)
    {
        if (_mapNameDictionary.TryGetValue(mapType, out string mapName))
        {
            string name = mapName;

            return name;
        }
        else
            return null;
    }

    private IEnumerator LoadMap(string mapName)
    {
        string mapPath = "Map/" + mapName;

        ResourceRequest request = Resources.LoadAsync<GameObject>(mapPath);

        LoadingUI loadUI = _uiEvent._loadUI;

        loadUI.gameObject.SetActive(true);

        StartCoroutine(loadUI.ImageBlink(request));

        while (!request.isDone)
        {
            yield return null;
        }
        
        if(_currentMap != null)
        {
            Destroy(_currentMap);
        }

        _currentMap = _di.InstantiatePrefab(request.asset as GameObject);

        _currentMap.SetActive(true);

        Stage currentStage = _currentMap.GetComponent<Stage>();

        if(currentStage != null)
        {
            currentStage.StartStage(_currentStageType);
            _uiEvent.OutLobbyMenuUI();
        }
    }

    IEnumerator LoadElite()
    {
        yield return new WaitWhile(() =>
        {
            Debug.Log("Elite의 데이터를 받아오는 중입니다.");
            return _dataManager.GetData("E241") == null;
        });

        var data = _dataManager.GetData("E241") as Monster_Elite;
        EliteChance = data.Value;
        Debug.Log("Elite의 확률을 성공적으로 받아왔습니다.");
        yield break;
    }
}
