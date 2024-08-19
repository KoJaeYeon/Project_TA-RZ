using Zenject;

public class PoolContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PoolManager>().FromNewComponentOnNewGameObject().AsSingle();
    }
}