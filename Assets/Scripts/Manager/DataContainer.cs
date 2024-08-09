using UnityEngine;
using Zenject;

public class DataContainer : MonoInstaller
{
    [SerializeField] GameObject googleSheetManagerPrefab; // 프리팹을 참조하는 public 변수

    public override void InstallBindings()
    {
        // GoogleSheetManager를 프리팹을 통해 바인딩
        Container.Bind<GoogleSheetManager>().FromComponentInNewPrefab(googleSheetManagerPrefab).AsSingle().NonLazy();
    }
}