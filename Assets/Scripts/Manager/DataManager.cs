using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    Dictionary<string, Data> _dataDictionary = new Dictionary<string, Data>();
    Dictionary<string, string> _stringDictionary = new Dictionary<string, string>();

    public void AddDataToDataDictionary(string idStr, Data stat)
    {
        if (!_dataDictionary.TryAdd(idStr, stat))
        {
            Debug.LogError($"ID : {idStr}가 데이터 딕셔너리에 추가 실패했습니다.");
        }
    }

    public void AddStringToStringDictionary(string idStr, string str)
    {
        if (!_stringDictionary.TryAdd(idStr, str))
        {
            Debug.LogError($"ID : {idStr}가 스트링 딕셔너리에 추가 실패했습니다.");
        }
    }

    public Dictionary<string, Data> Log()
    {
        return _dataDictionary;
    }
    public Dictionary<string, string> LogString()
    {
        return _stringDictionary;
    }

    /// <summary>
    /// 스탯을 받아오기 위한 함수
    /// </summary>
    /// <param name="idStr">받아올 스탯의 ID string 데이터</param>
    /// <returns></returns>
    public Stat GetStat(string idStr)
    {
        if (_dataDictionary.TryGetValue(idStr, out Data data))
        {
            var stat = data as Stat;
            return stat.DeepCopy();
        }
        else
        {
            Debug.LogError($"{idStr}을 딕셔너리에서 받아오는데 실패했습니다.");
            return null;
        }
    }
    /// <summary>
    /// 데이터만을 받아오기 위한 함수
    /// </summary>
    /// <param name="idStr">받아올 스탯의 ID string 데이터</param>
    public Data GetData(string idStr)
    {
        if (_dataDictionary.TryGetValue(idStr, out Data data))
        {
            return data;
        }
        else
        {
            Debug.LogError($"{idStr}을 딕셔너리에서 받아오는데 실패했습니다.");
            return null;
        }
    }

    /// <summary>
    /// String 데이터만을 받아오기 위한 함수
    /// </summary>
    /// <param name="idStr">받아올 스탯의 ID string 데이터</param>
    public string GetString(string idStr)
    {
        if (_stringDictionary.TryGetValue(idStr, out string str))
        {
            return str;
        }
        else
        {
            Debug.LogError($"{idStr}을 딕셔너리에서 받아오는데 실패했습니다.");
            return string.Empty;
        }
    }

    /// <summary>
    /// Json 데이터를 딕셔너리에 저장하기 위한 함수
    /// </summary>
    /// <param name="urlName">data를 받아온 URL의 이름</param>
    /// <param name="data">Json 데이터</param>
    public void ProcessData(string urlName, string data)
    {
        switch (urlName)
        {
            case "_PC_Stat_URL":
                Process_PC_Stat_Data(data);
                break;
            case "_PC_Level_URL":
                Process_PC_Level_Data(data);
                break;
            case "_PC_Skill_URL":
                Process_PC_Skill_Data(data);
                break;
            case "_PC_Attack_URL":
                Process_PC_Atatck_Data(data);
                break;
            case "_PC_Melee_URL":
                Process_PC_Melee_Data(data);
                break;
            case "_Monster_Stat_URL":
                Process_Monster_Stat_Data(data);
                break;
            case "_Monster_Ability_URL":
                Process_Monster_Ability_Data(data);
                break;
            case "_Boss_Skill_URL":
                Process_Boss_Skill_Data(data);
                break;
            case "_Map_Stat_URL":
                Process_Map_Stat_Data(data);
                break;
            case "_Map_Stage_Level_URL":
                Process_Map_Stage_Level_Data(data);
                break;
            case "_Map_Monster_Mix_URL":
                Process_Monster_Mix_Data(data);
                break;
            case "_String_Data_URL":
                Process_String_Data(data);
                break;
            default:
                Debug.LogError($"Unknown URL name: {urlName}");
                break;
        }
    }

    #region Data Parsing Process

    private void Process_PC_Stat_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item["ID"].ToString();
            int id = int.Parse(idStr.Substring(1));
            string type = item["Type"].ToString();

            float atkPower = ParseFloat(item["PC_Common_Atk_Power"]);
            float hp = ParseFloat(item["PC_Common_Hp"]);
            float moveSpeed = ParseFloat(item["PC_Common_Moving_Speed"]);
            int resouceOwnNum = ParseInt(item["PC_Common_Resource_OwnNum"]);
            float staminaGain = ParseFloat(item["PC_Common_StaminaGain"]);
            float drainStamina = ParseFloat(item["PC_Common_Drain_Stamina"]);
            float dashStamina = ParseFloat(item["PC_Common_Dash_Stamina"]);
            float drainMaxRange = ParseFloat(item[nameof(PC_Common_Stat.Drain_MaxRange)]);
            float rangeSpeed = ParseFloat(item[nameof(PC_Common_Stat.Range_Speed)]);
            float pullSpeed = ParseFloat(item[nameof(PC_Common_Stat.Pull_Speed)]);

            PC_Common_Stat stat = new PC_Common_Stat(id, type, atkPower, hp, moveSpeed, resouceOwnNum, staminaGain, drainStamina, dashStamina, drainMaxRange, rangeSpeed, pullSpeed);
            AddDataToDataDictionary(idStr, stat);
        }
    }

    private void Process_PC_Level_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(PC_Level.ID)].ToString();
            int levelMinRequire = ParseInt(item[nameof(PC_Level.Level_Min_Require)]);
            int levelConsumption = ParseInt(item[nameof(PC_Level.Level_Consumption)]);
            float levelAtkPowerMultiplier = ParseFloat(item[nameof(PC_Level.Level_Atk_Power_Multiplier)]);
            float levelAtkRangeMultiplier = ParseFloat(item[nameof(PC_Level.Level_Atk_Range_Multiplier)]);
            bool levelStiffIgnoring = bool.Parse(item[nameof(PC_Level.Level_Stiff_Ignoring)].ToString());

            PC_Level stat = new PC_Level(idStr, levelMinRequire, levelConsumption, levelAtkPowerMultiplier, levelAtkRangeMultiplier, levelStiffIgnoring);
            AddDataToDataDictionary(idStr, stat);
        }
    }
    
    private void Process_PC_Skill_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(PC_Skill.ID)].ToString();
            int skillGaugeConsumption = ParseInt(item[nameof(PC_Skill.Skill_Gauge_Consumption)]);
            int skillDuration = ParseInt(item[nameof(PC_Skill.Skill_Duration)]);
            List<float> skillValue = parseList<float>(item[nameof(PC_Skill.Skill_Value)]);

            PC_Skill skill = new PC_Skill(idStr, skillGaugeConsumption, skillDuration, skillValue);
            AddDataToDataDictionary(idStr, skill);
        }
    }

    private void Process_PC_Atatck_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(PC_Attack.ID)].ToString();
            float atkMultiplier = ParseFloat(item["PC_Type1_Atk_Multiplier"]);
            int arm0SkillGageGet = ParseInt(item["PC_Type1_Arm0_SkillGageGet"]);
            int arm1SkillGageGet = ParseInt(item["PC_Type1_Arm1_SkillGageGet"]);
            int arm2SkillGageGet = ParseInt(item["PC_Type1_Arm2_SkillGageGet"]);
            int arm3SkillGageGet = ParseInt(item["PC_Type1_Arm3_SkillGageGet"]);
            float atk4GageGetMaxT = ParseFloat(item["PC_Type1_Atk4_GageGetMaxT"]);
            float atk4GageKeepT = ParseFloat(item["PC_Type1_Atk4_GageKeepT"]);
            float abnStatusValue = ParseFloat(item["PC_Type1_AbnStatus_Value"]);

            PC_Attack attack = new PC_Attack(idStr, atkMultiplier, new int[] {arm0SkillGageGet,arm1SkillGageGet,arm2SkillGageGet,arm3SkillGageGet }, atk4GageGetMaxT, atk4GageKeepT, abnStatusValue);
            AddDataToDataDictionary(idStr, attack);
        }
    }

    private void Process_PC_Melee_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(PC_Melee.ID)].ToString();
            float atk4ChargeMaxT = ParseFloat(item[nameof(PC_Melee.Atk4_ChargeMaxT)]);
            float atk4NextChargeT = ParseFloat(item[nameof(PC_Melee.Atk4_NextChargeT)]);

            PC_Melee skill = new PC_Melee(idStr, atk4ChargeMaxT, atk4NextChargeT);
            AddDataToDataDictionary(idStr, skill);
        }
    }


    private void Process_Monster_Stat_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item["ID"].ToString();
            float hp = ParseFloat(item["Mon_Common_Stat_HP"]);
            float damage = ParseFloat(item["Mon_Common_Stat_Damage"]);
            float attackArea = ParseFloat(item["Mon_Common_Stat_AttackArea"]);
            int range = ParseInt(item["Mon_Common_Stat_Range"]);
            float detectArea = ParseFloat(item["Mon_Common_Stat_DetectArea"]);
            float detectTime = ParseFloat(item["Mon_Common_Stat_DetectTime"]);
            float movementSpeed = ParseFloat(item["Mon_Common_Stat_MovementSpeed"]);
            float cooldown = ParseFloat(item["Mon_Common_Stat_Cooldown"]);

            Monster_Stat stat = new Monster_Stat(idStr, hp, damage, attackArea, range, detectArea, detectTime, movementSpeed, cooldown);
            AddDataToDataDictionary(idStr, stat);
        }
    }

    private void Process_Monster_Ability_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(Monster_Ability.ID)].ToString();
            float stat_HPMag = ParseFloat(item[nameof(Monster_Ability.Stat_HPMag)]);
            float stat_DmgMag = ParseFloat(item[nameof(Monster_Ability.Stat_DmgMag)]);
            float stat_MSMag = ParseFloat(item[nameof(Monster_Ability.Stat_MSMag)]);
            float stat_CDMag = ParseFloat(item[nameof(Monster_Ability.Stat_CDMag)]);

            Monster_Ability ability = new Monster_Ability(idStr, stat_HPMag, stat_DmgMag, stat_MSMag, stat_CDMag);
            AddDataToDataDictionary(idStr, ability);
        }
    }

    private void Process_Boss_Skill_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(Boss_Skill.ID)].ToString();
            float skill_Casting = ParseFloat(item[nameof(Boss_Skill.Skill_Casting)]);
            float skill_Range = ParseFloat(item[nameof(Boss_Skill.Skill_Range)]);
            float skill_Damage = ParseFloat(item[nameof(Boss_Skill.Skill_Damage)]);

            Boss_Skill ability = new Boss_Skill(idStr, skill_Casting, skill_Range, skill_Damage);
            AddDataToDataDictionary(idStr, ability);
        }
    }


    private void Process_Map_Stat_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(Map_Stat.ID)].ToString();
            float stat_Multiply_Value = ParseFloat(item[nameof(Map_Stat.Stat_Multiply_Value)]);

            Map_Stat mapStat = new Map_Stat(idStr, stat_Multiply_Value);
            AddDataToDataDictionary(idStr, mapStat);
        }
    }

    private void Process_Map_Stage_Level_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(Map_Stage_Level.ID)].ToString();
            int level_Enemy_Create_2Panel_Count = ParseInt(item[nameof(Map_Stage_Level.Level_Enemy_Create_2Panel_Count)]);
            int level_Enemy_Create_6Panel_Count = ParseInt(item[nameof(Map_Stage_Level.Level_Enemy_Create_6Panel_Count)]);
            int level_Enemy_Create_9Panel_Count = ParseInt(item[nameof(Map_Stage_Level.Level_Enemy_Create_9Panel_Count)]);

            Map_Stage_Level mapStat = new Map_Stage_Level(idStr, level_Enemy_Create_2Panel_Count, level_Enemy_Create_6Panel_Count, level_Enemy_Create_9Panel_Count);
            AddDataToDataDictionary(idStr, mapStat);
        }
    }

    private void Process_Monster_Mix_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(Map_Monster_Mix.ID)].ToString();
            int mon_MonsterB = ParseInt(item[$"{nameof(Map_Monster_Mix.Mon_Monster)}B"]);
            int mon_MonsterC = ParseInt(item[$"{nameof(Map_Monster_Mix.Mon_Monster)}C"]);
            int mon_MonsterD = ParseInt(item[$"{nameof(Map_Monster_Mix.Mon_Monster)}D"]);
            int mon_MonsterA = ParseInt(item[$"{nameof(Map_Monster_Mix.Mon_Monster)}A"]);

            Map_Monster_Mix mapStat = new Map_Monster_Mix(idStr, new int[] { mon_MonsterB, mon_MonsterC, mon_MonsterD, mon_MonsterA });
            AddDataToDataDictionary(idStr, mapStat);
        }
    }

    private void Process_String_Data(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item[nameof(Map_Stat.ID)].ToString();
            string str = item["Korean"].ToString();

            AddStringToStringDictionary(idStr, str);
        }
    }
    #endregion

    #region Parse Data
    private List<T> parseList<T>(JToken token)
    {
        List<T> result = new List<T>();

        if (token != null && token.Type == JTokenType.String)
        {
            string tokenStr = token.ToString().Trim('{', '}');

            if (!string.IsNullOrWhiteSpace(tokenStr) && tokenStr != "-")
            {
                foreach (var item in tokenStr.Split(','))
                {
                    if (typeof(T) == typeof(float))
                    {
                        if (float.TryParse(item, out float parsedFloat))
                        {
                            result.Add((T)(object)parsedFloat);
                        }
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        if (int.TryParse(item, out int parsedInt))
                        {
                            result.Add((T)(object)parsedInt);
                        }
                    }
                }
            }
        }

        return result;
    }


    private float ParseFloat(JToken token)
    {
        float result;
        if (token == null || !float.TryParse(token.ToString(), out result))
        {
            result = 0.0f;
        }
        return result;
    }

    private int ParseInt(JToken token)
    {
        int result;
        if (token == null || !int.TryParse(token.ToString(), out result))
        {
            result = 0;
        }
        return result;
    }
    #endregion
}