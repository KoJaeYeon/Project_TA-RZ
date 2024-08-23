using Cinemachine;
using Zenject;

public class PlayerContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        if(FindObjectOfType<Player>() != null)
        {
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();

            Container.Bind<CameraRoot>().FromComponentInHierarchy().AsSingle();

            Container.Bind<DrainSystem>().FromComponentInHierarchy().AsSingle();
        }

        if(FindObjectOfType<CinemachineVirtualCamera>() != null)
        {
            Container.Bind<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();
        }
    }
}
