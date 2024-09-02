using Zenject;
using UnityEngine;

public class MapContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        if(FindObjectOfType<MapManager>() != null)
        {
            Container.Bind<MapManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
