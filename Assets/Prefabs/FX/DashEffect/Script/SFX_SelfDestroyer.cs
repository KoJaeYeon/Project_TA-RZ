// ReSharper disable once CheckNamespace
namespace QFX.SFX
{
    //사용안함
    public class SFX_SelfDestroyer : SFX_ControlledObject
    {
        public float LifeTime;

        public override void Run()
        {
            base.Run();
            Destroy(gameObject, LifeTime);
        }
    }
}