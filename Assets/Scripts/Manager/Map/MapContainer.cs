using Zenject;

public class MapContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        if(FindObjectOfType<MapManager>() != null)
        {
            Container.Bind<MapManager>().FromComponentInHierarchy().AsSingle();
        }
        else
        {
            Container.Bind<MapManager>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}
