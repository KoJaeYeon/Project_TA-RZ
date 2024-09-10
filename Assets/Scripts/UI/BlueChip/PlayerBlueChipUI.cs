using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum BlueChipID
{
    G201,
    G202, 
    G203,
    G204,
}

public class PlayerBlueChipUI : MonoBehaviour
{
    [Inject]
    private Player _player;
    [Inject]
    private DataManager _dataManager;

    [Header("CurrentBlueChipUI")]
    [SerializeField] private GameObject[] _currentBlueChipUI;

    private Dictionary<BlueChipID, GameObject> _currentBlueDictionary = new Dictionary<BlueChipID, GameObject>();   

    private void OnEnable()
    {
        OnSelectBlueChip();
    }

    private void OnSelectBlueChip()
    {
        for(int i = 0; i < _player.BluechipSkillLevels.Length; i++)
        {
            if (_player.BluechipSkillLevels[i] > 0)
            {
                AddUI(i, _player.BluechipSkillLevels[i]);
            }

        }
    }

    private void AddUI(int index, int blueLevel)
    {
        string id = index.ToString();

        BlueChipID blueID = (BlueChipID)Enum.Parse(typeof(BlueChipID), id);

        if (!_currentBlueDictionary.ContainsKey(blueID))
        {
            if(blueID is BlueChipID.G203)
            {
                foreach(var ui in _currentBlueDictionary)
                {
                    if(ui.Key == BlueChipID.G201 || ui.Key == BlueChipID.G202)
                    {
                        ui.Value.SetActive(false);
                    }
                }
            }

            _currentBlueChipUI[index].SetActive(true);

            var uiComponent = _currentBlueChipUI[index].GetComponent<CurrentBlueChipUI>();

            uiComponent.LoadBlueChipUI(blueID.ToString(), _dataManager);

            uiComponent.RequestLevel(blueLevel);

            _currentBlueDictionary.Add(blueID, _currentBlueChipUI[index]);
        }
        else
        {
            var component = _currentBlueDictionary[blueID].GetComponent<CurrentBlueChipUI>();

            component.RequestLevel(blueLevel);
        }

    }
}
