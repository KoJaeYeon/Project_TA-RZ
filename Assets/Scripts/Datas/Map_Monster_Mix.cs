public class Map_Monster_Mix : Data
{
    public string ID { get; }
    public int[] Mon_Monster { get; }

    public Map_Monster_Mix() : this("L301", new int[] {4,4,3,4 })
    {
    }

    public Map_Monster_Mix(
        string id,
        int[] mon_Monster)
    {
        ID = id;
        Mon_Monster = mon_Monster;
    }

    public override string ToString()
    {
        return $"ID: {ID}, mon_Monster";
    }
}
