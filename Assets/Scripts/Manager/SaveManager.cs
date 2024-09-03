using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void Save(int num)
    {
        var saveData = new Save_PlayerData();
        saveData.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        Debug.Log(Application.persistentDataPath);
        var json = JsonConvert.SerializeObject(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savedata" + num + ".json", json);
    }

    public Save_PlayerData Load(int num)
    {
        var json = File.ReadAllText(Application.persistentDataPath + "/savedata" + num + ".json");
        var loadData = JsonConvert.DeserializeObject<Save_PlayerData>(json);
        return loadData;
    }
}