public class Save_PlayerData
{
    public string saveTime {get; set;}
    public int money { get; set; } = 10;
    public bool BossKilled { get; set; } = false;
    public bool Charged { get; set; } = false;
    public bool NoHitBossKilled { get; set; } = false;
    public bool AllUnlocked { get; set; } = false;
    public bool RedChip { get; set; } = false;
    public bool EnemyKilled { get; set; } = false;
    public bool ResourceGet { get; set; } = false;

    public int[] passiveIndex { get; set; } = new int[6];
    public int Kill { get; set; } = 0;
    public int resource { get; set; } = 0;

}
