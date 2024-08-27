using System.Collections.Generic;

public class Map_Monster_Mix : Data
{
    public string ID { get; }
    public List<int> Mon_Monster { get; }

    public Map_Monster_Mix() : this("L301", new List<int> {4,4,3,4 })
    {
    }

    public Map_Monster_Mix(
        string id,
        List<int> mon_Monster)
    {
        ID = id;
        Mon_Monster = mon_Monster;
    }

    public override string ToString()
    {
        string Mon_Monster_String = Mon_Monster.Count > 0 ? string.Join(", ", Mon_Monster) : "None";
        return $"ID: {ID}, mon_Monster: {Mon_Monster_String}";
    }
}
