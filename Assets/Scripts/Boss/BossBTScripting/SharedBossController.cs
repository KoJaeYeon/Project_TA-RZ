namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedBossController : SharedVariable<BossController>
    {
        public static implicit operator SharedBossController(BossController value) { return new SharedBossController { Value = value }; }
    }
}