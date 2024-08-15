public class PC_Level : Data
{
    public string ID { get; } = "P501";
    public int Level_Min_Require { get;  } = 0;
    public int Level_Consumption { get;  } = 0;
    public float Level_Atk_Power_Multiplier { get;  } = 1.0f;
    public float Level_Atk_Range_Multiplier { get;  } = 1.0f;
    public bool Level_Stiff_Ignoring { get;  } = false;

    public PC_Level() { }

    public PC_Level(string id, int level_Min_Require, int level_Consumption, float level_Atk_Power_Multiplier, float level_Atk_Range_Multiplier, bool level_Stiff_Ignoring)
    {
        ID = id;
        Level_Min_Require = level_Min_Require;
        Level_Consumption = level_Consumption;
        Level_Atk_Power_Multiplier = level_Atk_Power_Multiplier;
        Level_Atk_Range_Multiplier = level_Atk_Range_Multiplier;
        Level_Stiff_Ignoring = level_Stiff_Ignoring;
    }

    public override Data DeepCopy()
    {
        return new PC_Level(ID, Level_Min_Require, Level_Consumption, Level_Atk_Power_Multiplier, Level_Atk_Range_Multiplier, Level_Stiff_Ignoring);
    }

    public override string ToString()
    {
        return $"ID: {ID}, Level Min Require: {Level_Min_Require}, Level Consumption: {Level_Consumption}, Attack Power Multiplier: {Level_Atk_Power_Multiplier}, Attack Range Multiplier: {Level_Atk_Range_Multiplier}, Stiff Ignoring: {Level_Stiff_Ignoring}";
    }
}
