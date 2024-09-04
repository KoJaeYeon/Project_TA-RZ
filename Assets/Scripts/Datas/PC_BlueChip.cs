using System.Collections.Generic;

public class PC_BlueChip : Data
{
    public string ID { get; }
    public string Path { get; }
    public string StringPath { get; }
    public float Att_damage { get; }
    public float Att_Damage_Lvup { get; }
    public float Chip_AttackArea { get; }
    public float Interval_time { get; }
    public float Chip_Lifetime { get; }
    public List<float> ValueList { get; }

    public PC_BlueChip() : this("G201", "Sprites/UI/Icon_PoisonAtk", "UI_Bluechip_PoisonAtk_Text", 1f, 1f, 1f, 1f, 1f, new List<float> { 0f, 0f })
    {
    }

    public PC_BlueChip(
        string id,
        string path,
        string stringPath,
        float att_damage,
        float att_damage_lvup,
        float chip_attackarea,
        float interval_time,
        float chip_lifetime,
        List<float> valuelist)
    { 
        ID = id;
        Path = path;
        StringPath = stringPath;
        Att_damage = att_damage;
        Att_Damage_Lvup = att_damage_lvup;
        Chip_AttackArea = chip_attackarea;
        Interval_time = interval_time;
        Chip_Lifetime = chip_lifetime;
        ValueList = valuelist;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Path: {Path}, StringPath: {StringPath},Att_damage: {Att_damage}, Att_Damage_Lvup: {Att_Damage_Lvup}" +
               $"Chip_AttackArea: {Chip_AttackArea}, Interval_time: {Interval_time}," +
               $"Chip_Lifetime: {Chip_Lifetime}, Value1: {ValueList[0]}, Value2: {ValueList[1]}";
    }

}
