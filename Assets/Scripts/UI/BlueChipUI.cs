using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BlueChipUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [Inject] public DataManager _dataManager { get; }
    [Inject] Player Player;

    [Header("BlueChipUI")]
    [SerializeField] private GameObject _poisonBluechipUI;
    [SerializeField] private GameObject _explosionBluechipUI;
    [SerializeField] private GameObject _magneticBluechipUI;

    [SerializeField] Blue_Select_Button[] blue_Select_Buttons;

    public Chest chest { get; set; }
    int _leftBlueChip = 0;

    private void OnEnable()
    {
        UIEvent.SetActivePlayerControl(false);
        ResetBlueChipUI();
        _leftBlueChip++;
    }

    private void OnDisable()
    {
        UIEvent.SetActivePlayerControl(true);
        _leftBlueChip = 0;
        if(chest != null)
        {
            chest.gameObject.SetActive(false);
        }
    }

    public void DeActiveBlueChipUI()
    {
        UIEvent.DeActiveBlueChipUI();
    }   

    public void OnClickPoisonBlueChipUI()
    {

    }

    public void OnClickExplosionBlueChipUI()
    {

    }

    public void OnClickMagneticBlueChipUI()
    {

    }

    public void QuestCleared()
    {
        _leftBlueChip++;
    }

    void ResetBlueChipUI()
    {
        for (int i = 0; i < blue_Select_Buttons.Length; i++)
        {
            int rand;
            if(i == 0)
            {
                rand =Random.Range(0, 2) == 0 ? Random.Range(1,5) : 5;
            }
            else
            {
                rand = Random.Range(1, 5);
            }

            blue_Select_Buttons[i].RefershButton($"G20{rand}");
        }
    }

    public void OnCLick_BlueChip(string blueID)
    {
        //블루칩 획득 기능
        switch (blueID)
        {





            case "G205":
                //재화 획득
                var pc_blue = _dataManager.GetData(blueID) as PC_BlueChip;
                Player.SavePlayerData.money += int.Parse(_dataManager.GetStringValue(pc_blue.StringPath));
                break;
        }

        _leftBlueChip--;
        if (_leftBlueChip == 0)
        {
            gameObject.SetActive(false);
            chest.SetActivePoratal();
        }
        else
        {
            ResetBlueChipUI();
        }
    }

}
