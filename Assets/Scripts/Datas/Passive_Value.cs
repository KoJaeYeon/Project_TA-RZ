using System.Collections.Generic;

public class Passive_Value : Data
{
    public string ID { get; }
    public List<float> Status_UP { get; }
    public int Status_1to2_NeedResource { get; }
    public int Status_2to3_NeedResource { get; }

    public Passive_Value() : this("G101", new List<float>{10, 20, 30}, 100, 1000)
    {
    }

    public Passive_Value(
        string id,
        List<float> status_Up,
        int status_1to2_NeedResource,
        int status_2to3_NeedResource)
    {
        ID = id;
        Status_UP = status_Up;
        Status_1to2_NeedResource = status_1to2_NeedResource;
        Status_2to3_NeedResource = status_2to3_NeedResource;
    }

    public override string ToString()
    {
        return $"ID: {ID}";
    }
}
