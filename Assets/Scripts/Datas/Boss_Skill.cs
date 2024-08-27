public class Boss_Skill : Data
{
    public string ID { get; }
    public float Skill_Casting { get; }
    public float Skill_Range { get; }
    public float Skill_Damage { get; }


    public Boss_Skill()
        : this("B101", 1, 0, 0)
    {
    }

    public Boss_Skill(string id, float skill_Casting, float skill_Range, float skill_Damage)
    {
        ID = id;
        Skill_Casting = skill_Casting;
        Skill_Range = skill_Range;
        Skill_Damage = skill_Damage;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Skill_Casting: {Skill_Casting}, Skill_Range: {Skill_Range}, Skill_Damage Area: {Skill_Damage}";
    }
}
