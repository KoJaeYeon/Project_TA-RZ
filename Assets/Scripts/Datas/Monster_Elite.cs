public class Monster_Elite : Data
{
    public string ID { get; }
    public float Value { get; }
    public Monster_Elite() : this("E241",0.3f)
    {

    }

    public Monster_Elite(
        string id,
        float value
        )
    {
        ID = id;
        Value = value;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Value: {Value}";
    }
}
