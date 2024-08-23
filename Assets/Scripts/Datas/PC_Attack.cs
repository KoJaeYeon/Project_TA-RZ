public class PC_Attack : Data
{
    public string ID { get; }
    public float Atk_Multiplier { get; }
    public int[] Arm_SkillGageGet { get; }
    public float Atk4_GageGetMaxT { get; }
    public float Atk4_GageKeepT { get; }
    public float AbnStatus_Value { get; }

    public PC_Attack() : this("A201", 1f, new int[] {2,3,4,5 } ,0f, 0f, 1f)
    {
    }

    public PC_Attack(
        string id,
        float atk_Multiplier,
        int[] arm_SkillGageGet,
        float atk4_GageGetMaxT,
        float atk4_GageKeepT,
        float abnStatus_Value)
    {
        ID = id;
        Atk_Multiplier = atk_Multiplier;
        Arm_SkillGageGet = arm_SkillGageGet;
        Atk4_GageGetMaxT = atk4_GageGetMaxT;
        Atk4_GageKeepT = atk4_GageKeepT;
        AbnStatus_Value = abnStatus_Value;
    }

    public override string ToString()
    {
        return $"ID: {ID}, Atk Multiplier: {Atk_Multiplier}, Arm0 Skill Gage Get: {Arm_SkillGageGet[0]}, " +
               $"Arm1 Skill Gage Get: {Arm_SkillGageGet[1]}, Arm2 Skill Gage Get: {Arm_SkillGageGet[2]}, " +
               $"Arm3 Skill Gage Get: {Arm_SkillGageGet[3]}, Atk4 Gage Get MaxT: {Atk4_GageGetMaxT}, " +
               $"Atk4 Gage KeepT: {Atk4_GageKeepT}, AbnStatus Value: {AbnStatus_Value}";
    }
}
