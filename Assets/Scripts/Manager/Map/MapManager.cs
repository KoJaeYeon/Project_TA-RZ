using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public enum MapType
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
    [SerializeField] private GameObject[] _mapArray;

    private Dictionary<MapType, GameObject> _mapDictionary;

    public float ProgressValue { get; set; }

    private StageType _currentStageType;
    private GameObject _currentMap;

    private void Awake()
    {
        InitializeMapManager();
    }

    private void Start()
    {
        _currentMap = GetMap(MapType.Lobby);
    }

    #region Initialize

    private void InitializeMapManager()
    {
        _mapDictionary = new Dictionary<MapType, GameObject>();

        if(_mapArray == null)
        {
            Debug.LogWarning("MapArray가 존재하지 않습니다.");
            return;
        }

        var map = new (MapType, GameObject)[]
        {
            (MapType.Lobby, _mapArray[0]),
            (MapType.Beginning, _mapArray[1]),
            (MapType.Middle, _mapArray[2]),
            (MapType.Final, _mapArray[3]),
            (MapType.Boss, _mapArray[4])
        };

        AddMap(map, _mapDictionary);
    }

    private void AddMap((MapType, GameObject)[] map,
        Dictionary<MapType, GameObject> mapDictionary = null)
    {
        foreach (var (mapType, mapObject) in map)
        {
            mapDictionary?.Add(mapType, mapObject);
        }
    }

    #endregion

    public void ChangeMap(StageType newStage)
    {
        _currentStageType = newStage;

        _currentMap.SetActive(false);

        if(ProgressValue <= 0.33f)
        {
            _currentMap = GetMap(MapType.Beginning);

            _currentMap.SetActive(true);

            Stage beginning = _currentMap.GetComponentInChildren<Stage>();

            beginning.StartStage(_currentStageType);
        }
        else if(ProgressValue <= 0.66f)
        {
            _currentMap = GetMap(MapType.Middle);

            _currentMap.SetActive(true);

            Stage middle = _currentMap.GetComponentInChildren<Stage>();

            middle.StartStage(_currentStageType);
        }
        else if(ProgressValue <= 0.99f)
        {
            _currentMap = GetMap(MapType.Final);

            _currentMap.SetActive(true);

            Stage final = _currentMap.GetComponentInChildren<Stage>();

            final.StartStage(_currentStageType);
        }
        else
        {
            Debug.Log("보스");
        }
    }

    public void LobbyMap()
    {
        GameObject lobby = GetMap(MapType.Lobby);

        _currentMap.SetActive(false);

        _currentMap = lobby;

        _currentMap.SetActive(true);
    }


    private GameObject GetMap(MapType mapType)
    {
        if (_mapDictionary.TryGetValue(mapType, out GameObject map))
        {
            GameObject mapPrefab = map;

            return mapPrefab;
        }
        else
            return null;
    }



 
}
