using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BlueChipUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [Inject] DataManager _dataManager;
    PC_BlueChip[] _bluechipData;
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        UIEvent.SetActivePlayerControl(false);

        StartCoroutine(LoadBlueChipData());
    }

    private IEnumerator LoadBlueChipData()
    {
        var blueChipdata = _dataManager.GetData("G201") as PC_BlueChip;
        _bluechipData = new PC_BlueChip[4];
        while (true)
        {
            yield return new WaitWhile(() =>
            {
                Debug.Log("블루칩 데이터를 받아오지 못했습니다.");
                return blueChipdata == null;
            });

            for(int i = 0; i < 4; i++)
            {
                blueChipdata = _dataManager.GetData($"G20{i + 1}") as PC_BlueChip;

                _bluechipData[i] = blueChipdata;    
            }

            for(int i = 0; i < _bluechipData.Length; i++)
            {
                if (_bluechipData[i] != null)
                {
                    Debug.Log(_bluechipData[i].Att_damage);
                    Debug.Log(_bluechipData[i].Att_Damage_Lvup);
                    Debug.Log(_bluechipData[i].Chip_AttackArea);
                    Debug.Log(_bluechipData[i].Interval_time);
                    Debug.Log(_bluechipData[i].Chip_Lifetime);
                }    
            }

            yield break;
        }
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIEvent.SetActivePlayerControl(true);
    }

    public void DeActiveBlueChipUI()
    {
        gameObject.SetActive(false);
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
    
}
