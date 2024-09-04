using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Blue_Select_Button : MonoBehaviour
{
    [SerializeField] BlueChipUI BlueChipUI;
    [SerializeField] Image BlueChip_Img;
    [SerializeField] TextMeshProUGUI BlueChip_Description_Text;
    string _blueID;
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClick_Button));
    }
    public void RefershButton(string blueID)
    {
        _blueID = blueID;
        var pc_blue = BlueChipUI._dataManager.GetData(blueID) as PC_BlueChip;
        BlueChip_Img.sprite = Resources.Load<Sprite>(pc_blue.Path);
        if(_blueID == "G205")
        {
            BlueChip_Description_Text.text = BlueChipUI._dataManager.GetStringValue(pc_blue.StringPath);
        }
        else
        {
            BlueChip_Description_Text.text = BlueChipUI._dataManager.GetString(pc_blue.StringPath);
        }
    }

    public void OnClick_Button()
    {
        BlueChipUI.OnCLick_BlueChip(_blueID);
    }
}
