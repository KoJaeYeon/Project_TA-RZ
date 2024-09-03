using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class ShopUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [Inject] DataManager _dataManager;
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] GameObject initial_Select_GameObject;
    [SerializeField] GameObject Frame;

    [Header("Passive Panel")]
    [SerializeField] TextMeshProUGUI Reinforce_Name;
    [SerializeField] TextMeshProUGUI Reinforce_Description;
    [SerializeField] GameObject Reinforce_Need_Image;
    [SerializeField] TextMeshProUGUI Reinforce_Need;

    GameObject previousGameObject;
    GameObject currentGameObject;
    private void OnEnable()
    {
        cancelAction.action.Enable();
        cancelAction.action.performed += OnCancel;
        UIEvent.SetActivePlayerControl(false);

        EventSystem.current.SetSelectedGameObject(initial_Select_GameObject);
        ShopUIRenew();
    }

    private void OnDisable()
    {
        cancelAction.action.performed -= OnCancel;
        cancelAction.action.Disable();
        UIEvent.SetActivePlayerControl(true);
    }

    private void Update()
    {
        currentGameObject = EventSystem.current.currentSelectedGameObject;
        if (previousGameObject != currentGameObject)
        {
            FormatText();
        }        
    }

    void FormatText()
    {
        Frame.transform.position = currentGameObject.transform.position;
        Reinforce_Name.text = _dataManager.GetString($"{currentGameObject.name}_Text");
        string valueID = _dataManager.GetStringValue($"{currentGameObject.name}_Text_Explain");
        string valueText = _dataManager.GetString($"{currentGameObject.name}_Text_Explain");

        var passive_Value = _dataManager.GetData(valueID) as Passive_Value;
        char lastChar = currentGameObject.name[currentGameObject.name.Length - 1];
        switch (lastChar)
        {
            case '1':
                Reinforce_Description.text = string.Format(valueText, passive_Value.Status_UP_1);
                Reinforce_Need_Image.SetActive(false);
                Reinforce_Need.gameObject.SetActive(false);
                break;
            case '2':
                Reinforce_Description.text = string.Format(valueText, passive_Value.Status_UP_2);
                Reinforce_Need.text = passive_Value.Status_1to2_NeedResource.ToString();
                Reinforce_Need_Image.SetActive(true);
                Reinforce_Need.gameObject.SetActive(true);
                break;
            case '3':
                Reinforce_Description.text = string.Format(valueText, passive_Value.Status_UP_3);
                Reinforce_Need.text = passive_Value.Status_2to3_NeedResource.ToString();
                Reinforce_Need_Image.SetActive(true);
                Reinforce_Need.gameObject.SetActive(true);
                break;
        }
    }

    void ShopUIRenew()
    {

    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        DeActiveShopUI();
    }

    public void DeActiveShopUI()
    {
        UIEvent.DeActiveShopUI();
    }
}
