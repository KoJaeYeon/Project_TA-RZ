using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StringData : MonoBehaviour
{
    [Inject]
    DataManager _dataManager;
    [SerializeField] string idStr;
    public string IDStr
    {
        get { return idStr; }
        set
        {
            idStr = value;
            StartCoroutine(LoadString());
        }
    }

    private void Awake()
    {
        
    }
    private void Start()
    {
        StartCoroutine(LoadString());
    }

    IEnumerator LoadString()
    {
        if(string.IsNullOrWhiteSpace(IDStr))
        {
            idStr = gameObject.name;
        }
        yield return new WaitWhile(() => (_dataManager.GetString(IDStr).Equals(string.Empty)));
        var tmp = GetComponent<TextMeshProUGUI>();
        if(tmp != null)
        {
            tmp.text = _dataManager.GetString(IDStr);
            Destroy(this);
        }
        else
        {
           var tmpT = GetComponent<Text>();
            tmpT.text = _dataManager.GetString(IDStr);
        }

    }
}
