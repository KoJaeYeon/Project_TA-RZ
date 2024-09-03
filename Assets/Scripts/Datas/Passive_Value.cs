public class Passive_Value : Data
{
    public string ID { get; }
    public float Status_UP_1 { get; }
    public float Status_UP_2 { get; }
    public float Status_UP_3 { get; }
    public float Status_1to2_NeedResource { get; }
    public float Status_2to3_NeedResource { get; }

    public Passive_Value() : this("G101", 10, 20, 30, 100, 1000)
    {
    }

    public Passive_Value(
        string id,
        float status_UP_1,
        float status_UP_2,
        float status_UP_3,
        float status_1to2_NeedResource,
        float status_2to3_NeedResource)
    {
        ID = id;
        Status_UP_1 = status_UP_1;
        Status_UP_2 = status_UP_2;
        Status_UP_3 = status_UP_3;
        Status_1to2_NeedResource = status_1to2_NeedResource;
        Status_2to3_NeedResource = status_2to3_NeedResource;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Status_UP_1 : {Status_UP_1}";
    }
}
