using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BlueChipUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [Inject] DataManager _dataManager;

    [Header("BlueChipUI")]
    [SerializeField] private GameObject _poisonBluechipUI;
    [SerializeField] private GameObject _explosionBluechipUI;
    [SerializeField] private GameObject _magneticBluechipUI;

    private void OnEnable()
    {
        UIEvent.SetActivePlayerControl(false);
    }

    //private IEnumerator LoadBlueChipData(string id)
    //{
    //    yield return new WaitWhile(() =>
    //    {
    //        Debug.Log($"{id} : 블루칩 데이터를 가져오지 못했습니다.");
    //        return _dataManager.GetData(id) == null;
    //    });

    //    var data = _dataManager.GetData(id) as PC_BlueChip;
    //}

    private void OnDisable()
    {
        UIEvent.SetActivePlayerControl(true);
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
    
}
