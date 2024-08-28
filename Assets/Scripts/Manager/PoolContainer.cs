using Zenject;
using UnityEngine;

public class PoolContainer : MonoInstaller
{
    [SerializeField]
    private GameObject _poolManager;

    public override void InstallBindings()
    {
        if (FindObjectOfType<PoolManager>() == null)
        {
            var managerInstance = Container.InstantiatePrefabForComponent<PoolManager>(_poolManager);
            DontDestroyOnLoad(managerInstance.gameObject);
            Container.BindInstance(managerInstance).AsSingle().NonLazy();
        }
        else
        {
            // 이미 존재하는 인스턴스를 바인딩
            Container.Bind<PoolManager>().FromInstance(FindObjectOfType<PoolManager>()).AsSingle().NonLazy();
        }


        Container.Bind<PoolManager>().FromNewComponentOnNewGameObject().AsSingle();
    }
}
