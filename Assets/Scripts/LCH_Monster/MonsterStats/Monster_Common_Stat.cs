public class Monster_Common_Stat : Stat
{
    public int Id { get; set; }
    public string Type { get; set; }

    public float Hp { get; set; }

    public float Damage { get; set; } 

    public float AttackArea { get; set; }

    public float Range { get; set; }

    public float DetectArea { get; set; }

    public float DetectTime { get; set; }

    public float MovementSpeed { get; set; }

    public float Cooldown { get; set; }

    public Monster_Common_Stat(int id, string type, float hp, float damage, float attackArea, float range, float detectArea, float detectTime, float movementSpeed, float cooldown)
    {
        Id = id;
        Type = type;
        Hp = hp;
        Damage = damage;
        AttackArea = attackArea;
        Range = range;
        DetectArea = detectArea;
        DetectTime = detectTime;
        MovementSpeed = movementSpeed;
        Cooldown = cooldown;
    }
    public override string ToString()
    {
        return $"ID: {Id}, Type: {Type}, Hp: {Hp}, Damage: {Damage}, AttackArea: {AttackArea}, Range: {Range}, DetectArea: {DetectArea}, DetectTime: {DetectTime}, MovementSpeed: {MovementSpeed}, Cooldown:{Cooldown}";
    }
    public override Stat DeepCopy()
    {
        throw new System.NotImplementedException();
    }
}
