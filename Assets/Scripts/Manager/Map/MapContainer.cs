using Zenject;
using UnityEngine;

public class MapContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        //안정성 추가
        if (FindObjectOfType<MapManager>() != null)
        {
            Container.Bind<MapManager>().FromComponentInHierarchy().AsSingle();
        }
        else
        {
            Debug.LogWarning("MapManager가 로드되지 않았습니다. 하이어라키에 존재하지 않습니다.");
            Container.Bind<MapManager>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}
