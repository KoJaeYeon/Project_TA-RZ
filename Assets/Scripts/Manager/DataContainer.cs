using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Cinemachine;

public class DataContainer : MonoInstaller
{
    [SerializeField] GameObject dataManagerPrefab; // 프리팹을 참조하는 public 변수
    [SerializeField] GameObject googleSheetManagerPrefab; // 프리팹을 참조하는 public 변수
    [SerializeField] GameObject saveManagerPrefab; // 프리팹을 참조하는 public 변수

    public override void InstallBindings()
    {
        // GoogleSheetManager가 이미 존재하는지 체크하고, 없으면 생성하고 바인딩
        if (FindObjectOfType<DataManager>() == null)
        {
            var managerInstance = Container.InstantiatePrefabForComponent<DataManager>(dataManagerPrefab);
            DontDestroyOnLoad(managerInstance.gameObject);
            Container.BindInstance(managerInstance).AsSingle().NonLazy();
        }
        else
        {
            // 이미 존재하는 인스턴스를 바인딩
            Container.Bind<DataManager>().FromInstance(FindObjectOfType<DataManager>()).AsSingle().NonLazy();
        }


        // GoogleSheetManager가 이미 존재하는지 체크하고, 없으면 생성하고 바인딩
        if (FindObjectOfType<GoogleSheetManager>() == null)
        {
            var managerInstance = Container.InstantiatePrefabForComponent<GoogleSheetManager>(googleSheetManagerPrefab);
            DontDestroyOnLoad(managerInstance.gameObject);
            Container.BindInstance(managerInstance).AsSingle().NonLazy();
        }
        else
        {
            // 이미 존재하는 인스턴스를 바인딩
            Container.Bind<GoogleSheetManager>().FromInstance(FindObjectOfType<GoogleSheetManager>()).AsSingle().NonLazy();
        }

        if (FindObjectOfType<SaveManager>() == null)
        {
            var managerInstance = Container.InstantiatePrefabForComponent<SaveManager>(saveManagerPrefab);
            DontDestroyOnLoad(managerInstance.gameObject);
            Container.BindInstance(managerInstance).AsSingle().NonLazy();
        }
        else
        {
            // 이미 존재하는 인스턴스를 바인딩
            Container.Bind<SaveManager>().FromInstance(FindObjectOfType<SaveManager>()).AsSingle().NonLazy();
        }
    }
}
