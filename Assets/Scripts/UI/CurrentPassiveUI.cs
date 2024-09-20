using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class CurrentPassiveUI : MonoBehaviour
{
    [Inject]
    private DataManager _dataManager;
    [Inject]
    private UIEvent _uiEvent;
    private Player _player;

    [Header("PassiveUI_Image")]
    [SerializeField] private Image _passiveImage;
    [Header("PassiveNameText")]
    [SerializeField] private TextMeshProUGUI _passiveNameText;
    [Header("PassiveDescription")]
    [SerializeField] private TextMeshProUGUI _descriptionText;

    private void OnEnable()
    {
        SetPlayer();
        CurrentPassiveUIOnEnable();
    }

    private void SetPlayer()
    {
        if(_player == null)
        {
            _player = _uiEvent.GetPlayer();
        }
    }

    private void CurrentPassiveUIOnEnable()
    {
        if (_player.SavePlayerData.PassiveDieMode == 2)
        {
            EquipPassive();
        }
        else
        {
            UnEquipPassive();
        }
    }

    private void EquipPassive()
    {
        _passiveImage.sprite = Resources.Load<Sprite>("Sprites/UI/Icon_diedieengine");
        StartCoroutine(LoadStringData("UI_Passive_Diedie_Name", _passiveNameText));
        StartCoroutine(LoadStringData("UI_Passive_Diedie_Text", _descriptionText));
    }

    private void UnEquipPassive()
    {
        _passiveImage.sprite = Resources.Load<Sprite>("Sprites/UI/x");
        _passiveNameText.text = "";
        _descriptionText.text = "";
    }

    private IEnumerator LoadStringData(string id, TextMeshProUGUI textMesh)
    {
        yield return new WaitWhile(() => _dataManager.GetString(id).Equals(string.Empty));

        if(textMesh != null)
        {
            textMesh.text = _dataManager.GetString(id);
        }
    }

}
