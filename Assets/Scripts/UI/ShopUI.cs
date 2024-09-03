using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class ShopUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [Inject] DataManager _dataManager;
    [Inject] Player _player;
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] GameObject initial_Select_GameObject;
    [SerializeField] GameObject Frame;

    [Header("Passive Panel")]
    [SerializeField] TextMeshProUGUI Reinforce_Name;
    [SerializeField] TextMeshProUGUI Reinforce_Description;
    [SerializeField] GameObject Reinforce_Need_Image;
    [SerializeField] TextMeshProUGUI Reinforce_Need;

    [SerializeField]
    Image[] ActivePassives_Img;

    [SerializeField] Sprite Frame_Sprite;

    GameObject previousGameObject;
    GameObject currentGameObject;

    List<GameObject> ActiveObjects = new List<GameObject>();
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
            previousGameObject = currentGameObject;
            FormatText();
        }        
    }

    void FormatText()
    {
        Frame.transform.position = currentGameObject.transform.position;

        if (currentGameObject.gameObject.name.StartsWith("Reinforce"))
        {
            char lastChar = currentGameObject.name[currentGameObject.name.Length - 1];
            int lastidx = lastChar - '0';
            if(lastidx - 1 < ActiveObjects.Count)
            {
                RenewPanel(ActiveObjects[lastidx-1]);
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
        else
        {
            RenewPanel(currentGameObject);
        }

    }

    void RenewPanel(GameObject selectGameObject)
    {
        Reinforce_Name.text = _dataManager.GetString($"{selectGameObject.name}_Text");
        string valueID = _dataManager.GetStringValue($"{selectGameObject.name}_Text_Explain");
        string valueText = _dataManager.GetString($"{selectGameObject.name}_Text_Explain");

        var passive_Value = _dataManager.GetData(valueID) as Passive_Value;
        char lastChar = selectGameObject.name[selectGameObject.name.Length - 1];

        switch (lastChar)
        {
            case '1':
                Reinforce_Description.text = string.Format(valueText, passive_Value.Status_UP[0]);
                Reinforce_Need_Image.SetActive(false);
                Reinforce_Need.gameObject.SetActive(false);
                break;
            case '2':
                Reinforce_Description.text = string.Format(valueText, passive_Value.Status_UP[1]);
                Reinforce_Need.text = passive_Value.Status_1to2_NeedResource.ToString();
                Reinforce_Need_Image.SetActive(true);
                Reinforce_Need.gameObject.SetActive(true);
                break;
            case '3':
                Reinforce_Description.text = string.Format(valueText, passive_Value.Status_UP[2]);
                Reinforce_Need.text = passive_Value.Status_2to3_NeedResource.ToString();
                Reinforce_Need_Image.SetActive(true);
                Reinforce_Need.gameObject.SetActive(true);
                break;
        }
    }

    void ShopUIRenew()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < ActiveObjects.Count)
            {
                var img = ActiveObjects[i].GetComponent<Image>();
                ActivePassives_Img[i].sprite = img.sprite;
                ActivePassives_Img[i].color = Color.red;
            }
            else
            {
                ActivePassives_Img[i].sprite = Frame_Sprite;
                ActivePassives_Img[i].color = Color.gray;
            }
        }
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        DeActiveShopUI();
    }

    public void DeActiveShopUI()
    {
        UIEvent.DeActiveShopUI();
    }

    public void AddActiveObject(GameObject gameObject)
    {
        if(ActiveObjects.Count >= 3) { return; }
        ActiveObjects.Add(gameObject);

        string valueID = _dataManager.GetStringValue($"{gameObject.name}_Text_Explain");
        var passive_Value = _dataManager.GetData(valueID) as Passive_Value;
        char lastChar = currentGameObject.name[currentGameObject.name.Length - 1];
        int lastidx = lastChar - '1';
        switch(valueID)
        {
            case "G101":
                break;
            case "G102":
                break;
            case "G103":
                break;
            case "G104":
                _player.PlayerPassiveData.addOwnNum += (int)passive_Value.Status_UP[lastidx];
                break;
            case "G105":
                break;
            case "G106":
                break;
        }

        ShopUIRenew();
    }
    public void OnSubmit_RemoveActiveObject(int index)
    {
        if (index > ActiveObjects.Count - 1) return;

        var activeObject = ActiveObjects[index];
        ActiveObjects.Remove(activeObject);
        var img = activeObject.GetComponent<Image>();
        img.color = Color.white;
        ShopUIRenew();
    }
}
