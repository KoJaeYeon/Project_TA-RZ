public class Map_Stat : Data
{
    public string ID { get; }
    public float Stat_Multiply_Value { get; }

    public Map_Stat() : this("E201", 1f)
    {
    }

    public Map_Stat(
        string id,
        float stat_StageMag)
    {
        ID = id;
        Stat_Multiply_Value = stat_StageMag;
    }

    public override string ToString()
    {
        return $"ID: {ID}, stat StageMag: {Stat_Multiply_Value}";
    }
}
