using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    Dictionary<string, Stat> _statDictionary = new Dictionary<string, Stat>();

    public void AddStatToStatDictionary(string idStr, Stat stat)
    {
        if (!_statDictionary.TryAdd(idStr, stat))
        {
            Debug.LogError($"ID : {idStr}가 스탯 딕셔너리에 추가 실패했습니다.");
        }
    }

    /// <summary>
    /// 스탯을 받아오기 위한 함수
    /// </summary>
    /// <param name="idStr">받아올 스탯의 ID string 데이터</param>
    /// <returns></returns>
    public Stat GetStat(string idStr)
    {
        if (_statDictionary.TryGetValue(idStr, out Stat stat))
        {
            return stat.DeepCopy();
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
                ProcessPCStatData(data);
                break;

            default:
                Debug.LogError($"Unknown URL name: {urlName}");
                break;
        }
    }

    private void ProcessPCStatData(string data)
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
            int trashOwnNum = ParseInt(item["PC_Common_Trash_OwnNum"]);
            float staminaGain = ParseFloat(item["PC_Common_StaminaGain"]);
            float drainStamina = ParseFloat(item["PC_Common_Drain_Stamina"]);
            float dashStamina = ParseFloat(item["PC_Common_Dash_Stamina"]);

            PC_Common_Stat stat = new PC_Common_Stat(id, type, atkPower, hp, moveSpeed, trashOwnNum, staminaGain, drainStamina, dashStamina);
            AddStatToStatDictionary(idStr, stat);
        }
    }

    private void ProcessMonsterStatData(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item["ID"].ToString().Substring(1);
            int id = int.Parse(idStr);
            string type = item["Type"].ToString();

            float attackPower = ParseFloat(item["Atk_Power"]);
            float health = ParseFloat(item["HP"]);
            float moveSpeed = ParseFloat(item["Move_Speed"]);
            int trashOwnNum = ParseInt(item["Trash_Own_Num"]);
            float staminaGain = ParseFloat(item["Stamina_Gain"]);
            float drainStamina = ParseFloat(item["Drain_Stamina"]);
            float dashStamina = ParseFloat(item["Dash_Stamina"]);

            PC_Common_Stat stat = new PC_Common_Stat(id, type, attackPower, health, moveSpeed, trashOwnNum, staminaGain, drainStamina, dashStamina);
            AddStatToStatDictionary(idStr, stat);
        }
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
