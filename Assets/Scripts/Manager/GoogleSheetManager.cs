using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Zenject;

public class GoogleSheetManager : MonoBehaviour
{
    const string _PCStat_URL = "https://script.google.com/macros/s/AKfycbwMq-9nTThTop6vAkoyyr5O7Hib2uMMmsrVtXkBH8sGU6ipDDW5Yk-vvDk7dgx4fuENvQ/exec";
    const string _PC_Level_URL = "https://script.google.com/macros/s/AKfycbxofCWapjAVsb8zGZfciCOHTEroZqIScbdic5u7Cyk3OPlbHyTw5VhHnGDRNc24Oho/exec";
    const string _PC_Skill_URL = "https://script.google.com/macros/s/AKfycby_rHh5Zgv5-9IzX5YZNHcBk2RFmHeGWN8WQnbYRxK3Z5qxHqry1jjmOJsWVsRnOCt_/exec";
    const string _PC_Attack_URL = "https://script.google.com/macros/s/AKfycbxRtDJSB7VeIkbcIeKxRJoBsDELpf4z9yDR2rlAVxILUH2Klk4UmCZt_p7KfuU9DyCp9g/exec";
    const string _Monster_Stat_URL = "https://script.google.com/macros/s/AKfycbzpl1LyCn504Guowz3d5ttyCyWPzqfhWvgNhYVg6kby4loIIf8Triar3JWCaxHh42be/exec";
    string data = string.Empty;

    [SerializeField] bool TryConnectSheet;

    [Inject] DataManager dataManager;

    void Start()
    {
        if (TryConnectSheet == false) return;

        StartCoroutine(RequestSJsonAPI(nameof(_PCStat_URL), _PCStat_URL));
        StartCoroutine(RequestSJsonAPI(nameof(_PC_Level_URL), _PC_Level_URL));
        StartCoroutine(RequestSJsonAPI(nameof(_PC_Skill_URL), _PC_Skill_URL));
        StartCoroutine(RequestSJsonAPI(nameof(_PC_Attack_URL), _PC_Attack_URL));
        StartCoroutine(RequestSJsonAPI(nameof(_Monster_Stat_URL), _Monster_Stat_URL));
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
