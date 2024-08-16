public class PC_Level : Data
{
    public string ID { get; }
    public int Level_Min_Require { get; }
    public int Level_Consumption { get; }
    public float Level_Atk_Power_Multiplier { get; }
    public float Level_Atk_Range_Multiplier { get; }
    public bool Level_Stiff_Ignoring { get; }

    public PC_Level() : this("P501", 0, 0, 1.0f, 1.0f, false)
    {
    }

    public PC_Level(string id, int level_Min_Require, int level_Consumption, float level_Atk_Power_Multiplier, float level_Atk_Range_Multiplier, bool level_Stiff_Ignoring)
    {
        ID = id;
        Level_Min_Require = level_Min_Require;
        Level_Consumption = level_Consumption;
        Level_Atk_Power_Multiplier = level_Atk_Power_Multiplier;
        Level_Atk_Range_Multiplier = level_Atk_Range_Multiplier;
        Level_Stiff_Ignoring = level_Stiff_Ignoring;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Level Min Require: {Level_Min_Require}, Level Consumption: {Level_Consumption}, Attack Power Multiplier: {Level_Atk_Power_Multiplier}, Attack Range Multiplier: {Level_Atk_Range_Multiplier}, Stiff Ignoring: {Level_Stiff_Ignoring}";
    }
}
