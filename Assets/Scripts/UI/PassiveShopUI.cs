using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class PassiveShopUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [Inject] DataManager _dataManager;
    [Inject] public Player _player { get; }
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] GameObject initial_Select_GameObject;

    [Header("UpBar")]
    [SerializeField] TextMeshProUGUI Money_Text;

    [Header("Select Panel")]
    [SerializeField] Button Engine;
    [SerializeField] GameObject UnlockPanel;

    [Header("View Panel")]
    [SerializeField] TextMeshProUGUI Reinforce_Name;
    [SerializeField] TextMeshProUGUI Reinforce_Description;
    [SerializeField] GameObject Reinforce_Image;
    [SerializeField] GameObject Reinforce_Need_Image;
    [SerializeField] TextMeshProUGUI Reinforce_Need;
    [SerializeField] TextMeshProUGUI Equip_Text;

    GameObject previousGameObject;
    GameObject currentGameObject;
    private void OnEnable()
    {
        cancelAction.action.Enable();
        cancelAction.action.performed += OnCancel;
        UIEvent.SetActivePlayerControl(false);
        UIEvent.SetActivePlayerUI(false);

        EventSystem.current.SetSelectedGameObject(initial_Select_GameObject);
        currentGameObject = initial_Select_GameObject;
        RenewAll();
    }

    private void OnDisable()
    {
        cancelAction.action.performed -= OnCancel;
        cancelAction.action.Disable();
        UIEvent.SetActivePlayerControl(true);
        UIEvent.SetActivePlayerUI(true);
    }

    private void Update()
    {
        currentGameObject = EventSystem.current.currentSelectedGameObject;
        if (previousGameObject != currentGameObject)
        {
            previousGameObject = currentGameObject;
            FormatText();
        }        
    }

    void FormatText()
    {
        if (currentGameObject.gameObject.name.StartsWith("Engine"))
        {
            var data = _dataManager.GetData("G251") as Passive2_Value;
            Reinforce_Name.text = "다이 다이 엔진";
            Reinforce_Description.text = "전체 공격력 2배/최대 체력 1/2";
            Reinforce_Need.text = data.Purchase_Fee.ToString();
            Reinforce_Image.SetActive(true);
            Equip_Text.transform.parent.gameObject.SetActive(true);
            if (_player.SavePlayerData.PassiveDieMode == 0)
            {
                Reinforce_Need_Image.gameObject.SetActive(true);
                UnlockPanel.SetActive(true);
            }
            else
            {
                Reinforce_Need_Image.gameObject.SetActive(false);
                UnlockPanel.SetActive(false);
            }
        }
        else
        {
            Reinforce_Name.text = "-";
            Reinforce_Description.text = "";
            Reinforce_Need.text = "";
            Reinforce_Image.SetActive(false);
            Reinforce_Need_Image.gameObject.SetActive(false);
            Equip_Text.transform.parent.gameObject.SetActive(false);
        }

    }

    public void RenewAll()
    {
        ShopUIRenew();

        //버튼 갱신
        Engine.onClick.RemoveAllListeners();
        switch (_player.SavePlayerData.PassiveDieMode)
        {
            case 0:
                Engine.onClick.AddListener(OnSubmit_Unlock);
                Equip_Text.text = _dataManager.GetString("UI_Passive_Abilities_Button_Text");
                break;
            case 1:
                Engine.onClick.AddListener(OnSubmit_Equip);
                Equip_Text.text = _dataManager.GetString("UI_Passive_Abilities_Equipped_Button_Text");
                break;
            case 2:
                Engine.onClick.AddListener(OnSubmit_UnEquip);
                Equip_Text.text = _dataManager.GetString("UI_Passive_Abilities_Release_Button_Text");
                break;
        }
    }

    void ShopUIRenew()
    {
        FormatText();

        Money_Text.text = _player.SavePlayerData.money.ToString();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        DeActivePassiveShopUI();
    }

    public void DeActivePassiveShopUI()
    {
        UIEvent.DeActivePassiveShopUI();
    }

    public void OnSubmit_Unlock()
    {
        var data = _dataManager.GetData("G251") as Passive2_Value;
        int needMoney = data.Purchase_Fee;
        if (_player.SavePlayerData.money < needMoney)
        {
            return;
        }


        _player.SavePlayerData.PassiveDieMode = 1;

        _player.SavePlayerData.money -= needMoney;
        Reinforce_Need_Image.gameObject.SetActive(false);
        Engine.onClick.RemoveAllListeners();
        Engine.onClick.AddListener(OnSubmit_Equip);
        Equip_Text.text = _dataManager.GetString("UI_Passive_Abilities_Equipped_Button_Text");

        ShopUIRenew();
    }

    public void OnSubmit_Equip()
    {
        _player.SavePlayerData.PassiveDieMode = 2;
        Engine.onClick.RemoveAllListeners();
        Engine.onClick.AddListener(OnSubmit_UnEquip);
        Equip_Text.text = _dataManager.GetString("UI_Passive_Abilities_Release_Button_Text");
        _player.CurrentHP = _player.HP;

        ShopUIRenew();
    }

    public void OnSubmit_UnEquip()
    {
        _player.SavePlayerData.PassiveDieMode = 1;
        Engine.onClick.RemoveAllListeners();
        Engine.onClick.AddListener(OnSubmit_Equip);
        Equip_Text.text = _dataManager.GetString("UI_Passive_Abilities_Equipped_Button_Text");
        _player.CurrentHP = _player.HP;

        ShopUIRenew();
    }
}
