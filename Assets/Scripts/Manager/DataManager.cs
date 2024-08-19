using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    Dictionary<string, Data> _dataDictionary = new Dictionary<string, Data>();

    public void AddDataToDataDictionary(string idStr, Data stat)
    {
        if (!_dataDictionary.TryAdd(idStr, stat))
        {
            Debug.LogError($"ID : {idStr}가 데이터 딕셔너리에 추가 실패했습니다.");
        }
    }

    public Dictionary<string, Data> Log()
    {
        return _dataDictionary;
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
    /// Json 데이터를 딕셔너리에 저장하기 위한 함수
    /// </summary>
    /// <param name="urlName">data를 받아온 URL의 이름</param>
    /// <param name="data">Json 데이터</param>
    public void ProcessData(string urlName, string data)
    {
        switch (urlName)
        {
            case "_PCStat_URL":
                Process_PCStat_Data(data);
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
            default:
                Debug.LogError($"Unknown URL name: {urlName}");
                break;
        }
    }

    private void Process_PCStat_Data(string data)
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

            PC_Common_Stat stat = new PC_Common_Stat(id, type, atkPower, hp, moveSpeed, resouceOwnNum, staminaGain, drainStamina, dashStamina);
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
            float atk4StiffT = ParseFloat(item["PC_Type1_Atk4_StiffT"]);
            float atk3KnockBackT = ParseFloat(item["PC_Type1_Atk3_KnockBackT"]);

            PC_Attack attack = new PC_Attack(idStr, atkMultiplier, arm0SkillGageGet, arm1SkillGageGet, arm2SkillGageGet, arm3SkillGageGet, atk4GageGetMaxT, atk4GageKeepT, atk4StiffT, atk3KnockBackT);
            AddDataToDataDictionary(idStr, attack);
        }
    }



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
}