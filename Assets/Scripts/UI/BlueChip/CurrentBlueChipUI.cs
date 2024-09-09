using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentBlueChipUI : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image _blueChipImage;

    [Header("BlueChipName")]
    [SerializeField] private TextMeshProUGUI _name;

    [Header("BlueChipDescription")]
    [SerializeField] private TextMeshProUGUI _description;

    public void LoadBlueChipUI(string blueID, DataManager dataManager)
    {
        SetStringData(blueID, dataManager);
    }

    private void SetStringData(string blueID, DataManager dataManager)
    {
        switch(blueID)
        {
            case "G201":
                StartCoroutine(LoadString(blueID, "UI_Bluechip_PoisonAtk_Name", dataManager));
                break;
            case "G202":
                StartCoroutine(LoadString(blueID, "UI_Bluechip_ExplosionAtk_Name", dataManager));
                break;
            case "G203":
                StartCoroutine(LoadString(blueID, "UI_Redchip_PoisonExplo_Name", dataManager));
                break;
            case "G204":
                StartCoroutine(LoadString(blueID, "UI_Bluechip__MagneticCharging_Name", dataManager));
                break;
        }
    }

    private IEnumerator LoadString(string blueID, string id, DataManager dataManager)
    {
        var blueChipData = dataManager.GetData(blueID) as PC_BlueChip;

        _blueChipImage.sprite = Resources.Load<Sprite>(blueChipData.Path);

        _description.text = dataManager.GetString(blueChipData.StringPath);

        yield return new WaitWhile(() => dataManager.GetString(id).Equals(string.Empty));

        if(_name != null)
        {
            _name.text = dataManager.GetString(id);
        }
    }
}
