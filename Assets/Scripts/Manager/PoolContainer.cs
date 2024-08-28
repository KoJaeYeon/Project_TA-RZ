using Zenject;
using UnityEngine;

public class PoolContainer : MonoInstaller
{
    [SerializeField]
    private GameObject _poolManager;
    [SerializeField]
    private GameObject UIPoolManager;

    public override void InstallBindings()
    {
        var managerInstance = Container.InstantiatePrefabForComponent<PoolManager>(_poolManager);
        Container.BindInstance(managerInstance).AsSingle().NonLazy();

        var UImanagerInstance = Container.InstantiatePrefabForComponent<UIPoolManager>(UIPoolManager);
        Container.BindInstance(UImanagerInstance).AsSingle().NonLazy();
    }
}
