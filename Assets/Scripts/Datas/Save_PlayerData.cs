public class Save_PlayerData
{
    public string saveTime { get; set; }
    public int money { get; set; } = 0;
    public bool BossKilled { get; set; } = false;
    public bool Charged { get; set; } = false;
    public bool NoHitBossKilled { get; set; } = false;
    public bool AllUnlockPassive { get; set; } = false;
    public bool RedChip { get; set; } = false;
    public bool EnemyKilled { get; set; } = false;
    public bool ResourceGet { get; set; } = false;
    public int[] passiveIndex { get; set; } = new int[6];
    public int PassiveDieMode { get; set; } = 0;
    public int Kill { get; set; } = 0;
    public int Resource { get; set; } = 0;

    public override string ToString()
    {
        return $"saveTime: {saveTime}, " +
               $"money: {money}, " +
               $"BossKilled: {BossKilled}, " +
               $"Charged: {Charged}, " +
               $"NoHitBossKilled: {NoHitBossKilled}, " +
               $"AllUnlocked: {AllUnlockPassive}, " +
               $"RedChip: {RedChip}, " +
               $"EnemyKilled: {EnemyKilled}, " +
               $"ResourceGet: {ResourceGet}, " +
               $"passiveIndex: [{string.Join(", ", passiveIndex)}], " +
               $"Kill: {Kill}, " +
               $"resource: {Resource}";
    }
}
