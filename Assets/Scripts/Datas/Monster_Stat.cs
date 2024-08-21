public class Monster_Stat : Stat
{
    public string ID { get; }
    public float HP { get; set; }
    public float Damage { get; set; }
    public float AttackArea { get; set; }
    public int Range { get; set; }
    public float DetectArea { get; set; }
    public float DetectTime { get; set; }
    public float MovementSpeed { get; set; }
    public float Cooldown { get; set; }

    public Monster_Stat()
        : this("E101", 50f, 10f, 2f, 2, 15f, 1f, 7f, 2f)
    {
    }

    public Monster_Stat(string id, float hp, float damage, float attackArea, int range, float detectArea, float detectTime, float movementSpeed, float cooldown)
    {
        ID = id;
        HP = hp;
        Damage = damage;
        AttackArea = attackArea;
        Range = range;
        DetectArea = detectArea;
        DetectTime = detectTime;
        MovementSpeed = movementSpeed;
        Cooldown = cooldown;
    }

    public override Stat DeepCopy()
    {
        return new Monster_Stat(ID, HP, Damage, AttackArea, Range, DetectArea, DetectTime, MovementSpeed, Cooldown);
    }

    public override string ToString()
    {
        return $"ID: {ID}, HP: {HP}, Damage: {Damage}, Attack Area: {AttackArea}, Range: {Range}, Detect Area: {DetectArea}, Detect Time: {DetectTime}, Movement Speed: {MovementSpeed}, Cooldown: {Cooldown}";
    }
}
