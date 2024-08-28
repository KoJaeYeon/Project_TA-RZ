using Zenject;
using UnityEngine;

public class MapContainer : MonoInstaller
{
    [SerializeField] private GameObject _mapManagerPrefab;

    public override void InstallBindings()
    {
        if(FindObjectOfType<MapManager>() == null)
        {
            var mapManager = Container.InstantiatePrefabForComponent<MapManager>(_mapManagerPrefab);
            DontDestroyOnLoad(mapManager.gameObject);
            Container.BindInstance(mapManager).AsSingle().NonLazy();
        }
        else
        {
            Container.Bind<MapManager>().FromInstance(FindAnyObjectByType<MapManager>()).AsSingle().NonLazy();
        }
    }
}
