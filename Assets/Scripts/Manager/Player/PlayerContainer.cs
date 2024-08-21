using Cinemachine;
using Zenject;

public class PlayerContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerManager>().AsSingle();

        Container.Bind<PlayerUIViewModel>().AsSingle();

        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();

        Container.Bind<CameraRoot>().FromComponentInHierarchy().AsSingle();

        Container.Bind<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();

        Container.Bind<DrainSystem>().FromComponentInHierarchy().AsSingle();
    }
}
