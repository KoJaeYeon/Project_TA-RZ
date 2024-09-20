using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public int saveIndex { get; set; } = -1;
    public void Save(Save_PlayerData save_PlayerData)
    {
        save_PlayerData.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        Debug.Log(Application.persistentDataPath);
        var json = JsonConvert.SerializeObject(save_PlayerData);
        File.WriteAllText(Application.persistentDataPath + "/savedata" + saveIndex + ".json", json);
    }

    public Save_PlayerData Load(int num)
    {
        try
        {
            var json = File.ReadAllText(Application.persistentDataPath + "/savedata" + num + ".json");
            var loadData = JsonConvert.DeserializeObject<Save_PlayerData>(json);
            return loadData;
        }
        catch
        {
            return null;
        }

    }
    //C:/Users/KGA/AppData/LocalLow/com_kga/Project_TA-RZ
}