public class Monster_Ability : Data
{
    public string ID { get; }
    public float Stat_HPMag { get; }
    public float Stat_DmgMag { get; }
    public float Stat_MSMag { get; }
    public float Stat_CDMag { get; }


    public Monster_Ability() : this("E211", 1f,1f,1f,1f)
    {
    }

    public Monster_Ability(
        string id,
        float stat_HPMag,
        float stat_DmgMag,
        float stat_MSMag,
        float stat_CDMag)
    {
        ID = id;
        Stat_HPMag = stat_HPMag;
        Stat_DmgMag = stat_DmgMag;
        Stat_MSMag = stat_MSMag;
        Stat_CDMag = stat_CDMag;
    }

    public override string ToString()
    {
        return $"ID: {ID}, stat HPMag: {Stat_HPMag}, stat DmgMag: {Stat_DmgMag}, stat MSMag: {Stat_MSMag}, stat CDMag: {Stat_CDMag}";
    }
}
