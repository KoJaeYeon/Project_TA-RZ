using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbxmrx_IStXECtqe-zd6LgsRpVkkw6_u-5NXWVThH-RBNl6YrCIK8IabP6Xnh_JU3w/exec";
    string data = string.Empty;

    void Start()
    {
        Debug.Log("Start Manager");
        StartCoroutine(RequestSJsonAPI(URL));
    }

    IEnumerator RequestSJsonAPI(string url)
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
            Debug.Log(data); // 받은 데이터를 로그로 출력
            ProcessData(data);
        }
    }

    void ProcessData(string data)
    {
        if (data == "임시제이슨")
        {
            // 특정행동
            Debug.Log("임시제이슨을 받았습니다.");
        }
        else
        {
            // 특정행동
            Debug.Log("임시제이슨이 아닙니다.");
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