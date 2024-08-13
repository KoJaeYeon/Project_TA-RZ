public class PC_Level : Data
{
    public string Id { get; set; }
    public int Level_Min_Require { get; set; }
    public int Level_Consumption { get; set; }
    public float Level_Atk_Power_Multiplier { get; set; }
    public float Level_Atk_Range_Multiplier { get; set; }
    public bool Level_Stiff_Ignoring { get; set; }

    public PC_Level() { }

    public PC_Level(string id, int level_Min_Require, int level_Consumption, float level_Atk_Power_Multiplier, float level_Atk_Range_Multiplier, bool level_Stiff_Ignoring)
    {
        Id = id;
        Level_Min_Require = level_Min_Require;
        Level_Consumption = level_Consumption;
        Level_Atk_Power_Multiplier = level_Atk_Power_Multiplier;
        Level_Atk_Range_Multiplier = level_Atk_Range_Multiplier;
        Level_Stiff_Ignoring = level_Stiff_Ignoring;
    }

    public override Data DeepCopy()
    {
        return new PC_Level(Id, Level_Min_Require, Level_Consumption, Level_Atk_Power_Multiplier, Level_Atk_Range_Multiplier, Level_Stiff_Ignoring);
    }

    public override string ToString()
    {
        return $"ID: {Id}, Level Min Require: {Level_Min_Require}, Level Consumption: {Level_Consumption}, Attack Power Multiplier: {Level_Atk_Power_Multiplier}, Attack Range Multiplier: {Level_Atk_Range_Multiplier}, Stiff Ignoring: {Level_Stiff_Ignoring}";
    }
}