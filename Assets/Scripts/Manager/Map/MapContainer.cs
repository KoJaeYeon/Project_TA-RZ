using Zenject;

public class MapContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MapManager>().FromComponentInHierarchy().AsSingle();
    }
}
