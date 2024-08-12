using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    Dictionary<string, Stat> _statDictionary = new Dictionary<string, Stat>();

    readonly string _PCStat_URL = "https://script.google.com/macros/s/AKfycbwMq-9nTThTop6vAkoyyr5O7Hib2uMMmsrVtXkBH8sGU6ipDDW5Yk-vvDk7dgx4fuENvQ/exec";

    public void AddStatToStatDictionary(string idStr, Stat stat)
    {
        if (_statDictionary.TryAdd(idStr, stat) == false)
        { 
            Debug.LogError($"ID : {idStr}가 스탯 딕셔너리에 추가 실패했습니다.");
        }
    }

    public Stat GetStat(string idStr)
    {
        if(_statDictionary.TryGetValue(idStr, out Stat stat))
        {
            return stat.DeepCopy();
        }
        else
        {
            Debug.LogError($"{idStr}을 딕셔너리에서 받아오는데 실패했습니다.");
            return null;
        }
        
    }

    void ProcessData(string data)
    {
        JArray jsonArray = JArray.Parse(data);

        foreach (var item in jsonArray)
        {
            string idStr = item["ID"].ToString().Substring(1);
            int id = int.Parse(idStr);
            string type = item["타입"].ToString();
            float attackPower = item["공격력"].ToObject<float>();
            float health = item["체력"].ToObject<float>();
            float moveSpeed = item["이동속도"].ToObject<float>();
            int ammoCapacity = item["자원 보유 총량"].ToObject<int>();
            float staminaRecoveryRate = item["스테미너 회복속도"].ToObject<float>();

            PC_Common_Stat stat = new PC_Common_Stat(id, type, attackPower, health, moveSpeed, ammoCapacity, staminaRecoveryRate);
            AddStatToStatDictionary(idStr,stat);
        }
    }
}
