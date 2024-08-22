public class PC_Melee : Data
{
    public string ID { get; }
    public float Atk4_ChargeMaxT { get; }
    public float Atk4_NextChargeT { get; }

    public PC_Melee() : this("A500", 6,1)
    {
    }

    public PC_Melee(
        string id,
        float atk4_ChargeMaxT,
        float atk4_NextChargeT)
    {
        ID = id;
        Atk4_ChargeMaxT = atk4_ChargeMaxT;
        Atk4_NextChargeT = atk4_NextChargeT;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Atk4 ChargeMaxT: {Atk4_ChargeMaxT},Atk4 NextChargeT: {Atk4_NextChargeT}";
    }
}
