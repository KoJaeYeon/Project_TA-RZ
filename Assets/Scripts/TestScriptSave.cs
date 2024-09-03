using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using Zenject;

public class TestScriptSave : MonoBehaviour
{
    [Inject] SaveManager SaveManager;

    Save_PlayerData save = new Save_PlayerData();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F1))
        {
            SaveManager.Save(0);
        }    
    }
}
