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

        }
        else
        {
            Reinforce_Name.text = "-";
            Reinforce_Description.text = "";
            Reinforce_Need.text = "";
            Reinforce_Need_Image.SetActive(false);
            Reinforce_Need.gameObject.SetActive(false);
        }

    }

    public void RenewAll()
    {
        ShopUIRenew();

        //버튼 갱신
    }

    void ShopUIRenew()
    {

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

    public bool UnlockNextButton()
    {

        return true;
    }


    public void EquipActiveObject()
    {

        ShopUIRenew();
    }

    public void UnEquipActiveObject()
    {

        ShopUIRenew();
    }
    public void OnSubmit_RemoveActiveObject(int index)
    {

        ShopUIRenew();
    }
}
