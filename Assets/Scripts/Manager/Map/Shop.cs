using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour, IInteractable
{
    [Inject] UIEvent UIEvent;
    [Inject] DataManager _dataManager;
    string returnText = string.Empty;

    private void Awake()
    {
        StartCoroutine(LoadString("UI_EternityReinforce_Ready_Text"));
    }

    public string GetText()
    {
        return returnText;
    }

    public void Interact()
    {
        UIEvent.ActiveShopUI();
    }

    IEnumerator LoadString(string IDStr)
    {
        yield return new WaitWhile(() => (_dataManager.GetString(IDStr).Equals(string.Empty)));
        returnText = _dataManager.GetString(IDStr);
    }
}