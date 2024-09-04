using Zenject;

public class UIContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UIEvent>().AsSingle();
    }
}
