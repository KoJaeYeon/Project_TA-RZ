public class Map_Stage_Level : Data
{
    public string ID { get; }
    public int Level_Enemy_Create_2Panel_Count { get; }
    public int Level_Enemy_Create_6Panel_Count { get; }
    public int Level_Enemy_Create_9Panel_Count { get; }

    public Map_Stage_Level() : this("L101", 1,3,5)
    {
    }

    public Map_Stage_Level(
        string id,
        int level_Enemy_Create_2Panel_Count,
        int level_Enemy_Create_6Panel_Count,
        int level_Enemy_Create_9Panel_Count)
    {
        ID = id;
        Level_Enemy_Create_2Panel_Count = level_Enemy_Create_2Panel_Count;
        Level_Enemy_Create_6Panel_Count = level_Enemy_Create_6Panel_Count;
        Level_Enemy_Create_9Panel_Count = level_Enemy_Create_9Panel_Count;
    }

    public override string ToString()
    {
        return $"ID: {ID}, level_Enemy_Create_2Panel_Count: {Level_Enemy_Create_2Panel_Count}, level_Enemy_Create_6Panel_Count: {Level_Enemy_Create_6Panel_Count}, level_Enemy_Create_9Panel_Count: {Level_Enemy_Create_9Panel_Count}";
    }
}
