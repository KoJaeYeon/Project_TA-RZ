using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Zenject;
using System.IO;

public class GoogleSheetManager : MonoBehaviour
{
    const string _PC_Stat_URL = "https://script.google.com/macros/s/AKfycbwMq-9nTThTop6vAkoyyr5O7Hib2uMMmsrVtXkBH8sGU6ipDDW5Yk-vvDk7dgx4fuENvQ/exec";
    const string _PC_Level_URL = "https://script.google.com/macros/s/AKfycbxofCWapjAVsb8zGZfciCOHTEroZqIScbdic5u7Cyk3OPlbHyTw5VhHnGDRNc24Oho/exec";
    const string _PC_Skill_URL = "https://script.google.com/macros/s/AKfycby_rHh5Zgv5-9IzX5YZNHcBk2RFmHeGWN8WQnbYRxK3Z5qxHqry1jjmOJsWVsRnOCt_/exec";
    const string _PC_Attack_URL = "https://script.google.com/macros/s/AKfycbxRtDJSB7VeIkbcIeKxRJoBsDELpf4z9yDR2rlAVxILUH2Klk4UmCZt_p7KfuU9DyCp9g/exec";
    const string _PC_Melee_URL = "https://script.google.com/macros/s/AKfycbyn9y5T39Pu3D_aNJmjO_UpBPyJcL3U7jcvmN92shIOLghxTY5HJL5_k88YhxhUG2HQ/exec";
    const string _Monster_Stat_URL = "https://script.google.com/macros/s/AKfycbzpl1LyCn504Guowz3d5ttyCyWPzqfhWvgNhYVg6kby4loIIf8Triar3JWCaxHh42be/exec";
    const string _Monster_Ability = "https://script.google.com/macros/s/AKfycbxpGKWuflt68ZnuXWpqoYMQM_3-gr6d-HtHLYmVLS9ZnBU4xjhO3ysaiK_hrs6tNwQ1/exec";
    const string _Map_Stat_URL = "https://script.google.com/macros/s/AKfycbz1OvZKBC5yz-p_UeX9163KMK0T5FrcAdlBHLxn-XJZzQ7YYZMM7TX5Jh0C_KaIDaf2/exec";
    const string _Map_Monster_Mix_URL = "https://script.google.com/macros/s/AKfycbzUwoyAd5bnZIs87lu6gskqM7IGkcA4AxV9k3knroFsN4IzUPAuHKZXAPi0r8ll4KCV/exec";
    const string _String_Data_URL = "https://script.google.com/macros/s/AKfycbwc6eo7YQ2DYoRLU3pPmsCELjTKwbStfeQH5AcsEhQlC_2xEDIZEHRmgBVwEJmUMNs/exec";

    
    string data = string.Empty;

    [SerializeField] bool TryConnectSheet;
    [SerializeField] bool TryLoadData;

    [Inject] DataManager dataManager;

    void Start()
    {
        if(TryLoadData)
        {
            StartCoroutine(SaveJsonData(nameof(_PC_Stat_URL), _PC_Stat_URL));
            StartCoroutine(SaveJsonData(nameof(_PC_Level_URL), _PC_Level_URL));
            StartCoroutine(SaveJsonData(nameof(_PC_Skill_URL), _PC_Skill_URL));
            StartCoroutine(SaveJsonData(nameof(_PC_Attack_URL), _PC_Attack_URL));
            StartCoroutine(SaveJsonData(nameof(_PC_Melee_URL), _PC_Melee_URL));
            StartCoroutine(SaveJsonData(nameof(_Monster_Stat_URL), _Monster_Stat_URL));
            StartCoroutine(SaveJsonData(nameof(_Monster_Ability), _Monster_Ability));
            StartCoroutine(SaveJsonData(nameof(_Map_Stat_URL), _Map_Stat_URL));
            StartCoroutine(SaveJsonData(nameof(_Map_Monster_Mix_URL), _Map_Monster_Mix_URL));
            StartCoroutine(SaveJsonData(nameof(_String_Data_URL), _String_Data_URL));
            return;
        }

        if (TryConnectSheet == false)
        {            
            RequestJsonRead(nameof(_PC_Stat_URL));
            RequestJsonRead(nameof(_PC_Level_URL));
            RequestJsonRead(nameof(_PC_Skill_URL));
            RequestJsonRead(nameof(_PC_Attack_URL));
            RequestJsonRead(nameof(_PC_Melee_URL));
            RequestJsonRead(nameof(_Monster_Stat_URL));
            RequestJsonRead(nameof(_Monster_Ability));
            RequestJsonRead(nameof(_Map_Stat_URL));
            RequestJsonRead(nameof(_Map_Monster_Mix_URL));
            RequestJsonRead(nameof(_String_Data_URL));
        }
        else
        {
            StartCoroutine(RequestSJsonAPI(nameof(_PC_Stat_URL), _PC_Stat_URL));
            StartCoroutine(RequestSJsonAPI(nameof(_PC_Level_URL), _PC_Level_URL));
            StartCoroutine(RequestSJsonAPI(nameof(_PC_Skill_URL), _PC_Skill_URL));
            StartCoroutine(RequestSJsonAPI(nameof(_PC_Attack_URL), _PC_Attack_URL));
            StartCoroutine(RequestSJsonAPI(nameof(_PC_Melee_URL), _PC_Melee_URL));
            StartCoroutine(RequestSJsonAPI(nameof(_Monster_Stat_URL), _Monster_Stat_URL));
            StartCoroutine(RequestSJsonAPI(nameof(_Monster_Ability), _Monster_Ability));
            StartCoroutine(RequestSJsonAPI(nameof(_Map_Stat_URL), _Map_Stat_URL));
            StartCoroutine(RequestSJsonAPI(nameof(_Map_Monster_Mix_URL), _Map_Monster_Mix_URL));
            StartCoroutine(RequestSJsonAPI(nameof(_String_Data_URL), _String_Data_URL));
        }
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            var data = dataManager.Log();
            foreach (var item in data)
            {
                Debug.Log(item.Value);
            }
            var stringData = dataManager.LogString();
            foreach (var item in stringData)
            {
                Debug.Log(item.Value);
            }
        }        
    }

    public IEnumerator RequestSJsonAPI(string urlName ,string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            data = www.downloadHandler.text;
            Debug.Log(FormatJson(data)); // 받은 데이터를 보기 좋게 포맷하여 로그로 출력
            dataManager.ProcessData(urlName, data);
        }
    }

    public IEnumerator SaveJsonData(string urlName ,string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            data = www.downloadHandler.text;
            var path = Path.Combine(Application.dataPath, $"Resources/Datas/{urlName}.json");
            File.WriteAllText(path, data);
            Debug.Log($"{path}위치에 {urlName} 저장 완료");
        }
    }

    public void RequestJsonRead(string urlName)
    {
        var jsonData = Resources.Load<TextAsset>($"Datas/{urlName}");
        dataManager.ProcessData(urlName, jsonData.text);
    }

    string FormatJson(string json)
    {
        try
        {
            JArray jsonArray = JArray.Parse(json);
            string formattedJson = jsonArray.ToString(Newtonsoft.Json.Formatting.Indented);
            return formattedJson;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to format JSON: " + ex.Message);
            return json;
        }
    }
}



#region WebCode
/*
function doGet(e) {
  var sheet = SpreadsheetApp.getActive();
  var nse = sheet.getSheetByName('시트2');
  var data = [];
  var rlen = nse.getLastRow();
  var clen = nse.getLastColumn();
  var rows = nse.getRange(1, 1, rlen, clen).getValues();
  for (var i = 0; i < rows.length; i++) {
    var datarow = rows[i];
    var record = {};
    for (var j = 1; j < clen; j++) {
      record[rows[0][j]] = datarow[j];
    }
    data.push(record);
  }
  console.log(data);
  var result = JSON.stringify(data);
  return ContentService.createTextOutput(result).setMimeType(ContentService.MimeType.JSON);
}
 */
#endregion
