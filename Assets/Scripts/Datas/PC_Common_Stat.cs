public class PC_Common_Stat : Stat
{
    public int Id { get; set; }
    public string Type { get; set; }
    public float Atk_Power { get; set; }
    public float HP { get; set; }
    public float Move_Speed { get; set; }
    public int Trash_Own_Num { get; set; }
    public float Stamina_Gain { get; set; }
    public float Drain_Stamina { get; set; }
    public float Dash_Stamina { get; set; }

    public PC_Common_Stat(int id, string type, float atk_Power, float hp, float move_Speed, int trash_Own_Num, float stamina_Gain, float drain_Stamina, float dash_Stamina)
    {
        Id = id;
        Type = type;
        Atk_Power = atk_Power;
        HP = hp;
        Move_Speed = move_Speed;
        Trash_Own_Num = trash_Own_Num;
        Stamina_Gain = stamina_Gain;
        Drain_Stamina = drain_Stamina;
        Dash_Stamina = dash_Stamina;
    }

    public override Stat DeepCopy()
    {
        return new PC_Common_Stat(Id, Type, Atk_Power, HP, Move_Speed, Trash_Own_Num, Stamina_Gain, Drain_Stamina, Dash_Stamina);
    }

    public override string ToString()
    {
        return $"ID: {Id}, Type: {Type}, Attack Power: {Atk_Power}, HP: {HP}, Move Speed: {Move_Speed}, Trash Own Num: {Trash_Own_Num}, Stamina Gain: {Stamina_Gain}, Drain Stamina: {Drain_Stamina}, Dash Stamina: {Dash_Stamina}";
    }
}
