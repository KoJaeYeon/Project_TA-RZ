using Zenject;
using UnityEngine;

public class PoolContainer : MonoInstaller
{
    [SerializeField]
    private GameObject _poolManager;

    public override void InstallBindings()
    {
            var managerInstance = Container.InstantiatePrefabForComponent<PoolManager>(_poolManager);
            Container.BindInstance(managerInstance).AsSingle().NonLazy();
    }
}
