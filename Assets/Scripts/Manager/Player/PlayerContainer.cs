using Zenject;

public class PlayerContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerManager>().AsSingle();

        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
    }
}
