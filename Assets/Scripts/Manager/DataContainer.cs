using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class DataContainer : MonoInstaller
{
    [SerializeField] GameObject googleSheetManagerPrefab; // 프리팹을 참조하는 public 변수

    public override void InstallBindings()
    {
        // 빈 Dictionary를 생성하고 바인딩
        Container.Bind<Dictionary<int, Stat>>().AsSingle().NonLazy();

        // GoogleSheetManager를 프리팹에서 인스턴스화하고 바인딩
        Container.Bind<GoogleSheetManager>().FromComponentInNewPrefab(googleSheetManagerPrefab).AsSingle().NonLazy();
    }
}
