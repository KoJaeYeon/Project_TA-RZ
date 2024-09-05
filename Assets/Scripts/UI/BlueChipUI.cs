using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BlueChipUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [Inject] public DataManager _dataManager { get; }
    [Inject] Player Player;

    [SerializeField] private BlueChipUI_SelectPanel _selectPanel;

    [Header("BlueChipUI")]
    [SerializeField] private GameObject _poisonBluechipUI;
    [SerializeField] private GameObject _explosionBluechipUI;
    [SerializeField] private GameObject _magneticBluechipUI;

    [SerializeField] Blue_Select_Button[] blue_Select_Buttons;

    public Chest chest { get; set; }
    int _leftBlueChip = 0;

    private bool isFirstReward = false;

    private void OnEnable()
    {
        UIEvent.SetActivePlayerControl(false);
        isFirstReward = true;
        ResetBlueChipUI();
        _leftBlueChip++;
    }

    private void OnDisable()
    {
        UIEvent.SetActivePlayerControl(true);
        _leftBlueChip = 0;
        if (chest != null)
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
        if (isFirstReward)
        {
            int reward;

            reward = Random.Range(0, 2);

            if (reward == 0)
            {
                RewardBlueChipUI(reward);
            }
            else
            {
                RewardBlueChipUI(reward);
            }
            isFirstReward = false;
        }
        else
        {
            RewardBlueChipUI();
            isFirstReward = false;

        }
    }
    private void RewardBlueChipUI(int random = 1)
    {
        switch (random)
        {
            case 0:
                blue_Select_Buttons[0].gameObject.SetActive(false);
                blue_Select_Buttons[1].RefershButton("G205");
                blue_Select_Buttons[2].gameObject.SetActive(false);
                break;

            case 1:
                blue_Select_Buttons[0].gameObject.SetActive(true);
                blue_Select_Buttons[2].gameObject.SetActive(true);

                List<int> list = new List<int>();

                for (int i = 0; i < Player.BluechipSkillLevels.Length; i++)
                {
                    if (Player.BluechipSkillLevels[i] < 2)
                    {
                        if (i == 2)
                        {
                            if (Player.BluechipSkillLevels[0] + Player.BluechipSkillLevels[1] < 4 || Player.BluechipSkillLevels[2] == 1)
                            {
                                continue;
                            }
                        }
                        list.Add(i + 1);
                    }
                }
                int count = list.Count;
                count = count <= 3 ? count : 3;
                foreach (var i in blue_Select_Buttons)
                {
                    i.gameObject.SetActive(false);
                }
                for (int i = 0; i < count; i++)
                {
                    int rand;

                    rand = Random.Range(0, list.Count);
                    int targetInt = list[rand];
                    list.Remove(list[rand]);
                    blue_Select_Buttons[i].RefershButton($"G20{targetInt}");
                }
                break;
        }

        _selectPanel.OnFocusBlueChipBtn();
    }

    public void OnCLick_BlueChip(string blueID)
    {
        //블루칩 획득 기능
        switch (blueID)
        {
            case "G201":
                Player.BluechipSkillLevels[0]++;
                break;
            case "G202":
                Player.BluechipSkillLevels[1]++;
                break;
            case "G203":
                Player.BluechipSkillLevels[2]++;
                break;
            case "G204":
                Player.BluechipSkillLevels[3]++;
                Player.IsPlayerFourthAttackDrainAvailable = true;
                break;
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
