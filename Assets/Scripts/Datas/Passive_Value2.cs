using System.Collections.Generic;

public class Passive2_Value : Data
{
    public string ID { get; }
    public int Purchase_Fee { get; }
    public List<float> Value { get; }

    public Passive2_Value() : this("G251", 10000, new List<float>{2,0.5f})
    {
    }

    public Passive2_Value(
        string id,
        int purchase_Fee,
        List<float> value)
    {
        ID = id;
        Purchase_Fee = purchase_Fee;
        Value = value;
    }

    public override string ToString()
    {
        return $"ID: {ID}";
    }
}
