using System.Collections.Generic;

public class PC_Skill : Data
{
    public string ID { get; }
    public int Skill_Gauge_Consumption { get; }
    public int Skill_Duration { get; }
    public List<float> Skill_Value { get; }

    public PC_Skill() : this("S201", 0, 3, new List<float>() { 1.5f })
    {
    }

    public PC_Skill(string id, int skillGaugeConsumption, int skillDuration, List<float> skillValue)
    {
        ID = id;
        Skill_Gauge_Consumption = skillGaugeConsumption;
        Skill_Duration = skillDuration;
        Skill_Value = skillValue ?? new List<float>();
    }

    public override string ToString()
    {
        string skillValueString = Skill_Value.Count > 0 ? string.Join(", ", Skill_Value) : "None";
        return $"ID: {ID}, Skill Gauge Consumption: {Skill_Gauge_Consumption}, Skill Duration: {Skill_Duration}, Skill Value: {skillValueString}";
    }
}
