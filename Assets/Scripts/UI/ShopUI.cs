using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class ShopUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [Inject] DataManager _dataManager;
    [Inject] public Player _player { get; }
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] GameObject initial_Select_GameObject;
    [SerializeField] GameObject Frame;

    [Header("UpBar")]
    [SerializeField] TextMeshProUGUI Money_Text;

    [Header("Passive Panel")]
    [SerializeField] TextMeshProUGUI Reinforce_Name;
    [SerializeField] TextMeshProUGUI Reinforce_Description;
    [SerializeField] GameObject Reinforce_Need_Image;
    [SerializeField] TextMeshProUGUI Reinforce_Need;

    [Header("Active Panel")]
    [SerializeField]
    Image[] ActivePassives_Img;
    [SerializeField]
    TextMeshProUGUI[] ActivePassives_Text;

    [SerializeField]
    PassiveButton[] passiveButtons;

    [SerializeField] Sprite Frame_Sprite;

    GameObject previousGameObject;
    GameObject currentGameObject;

    List<GameObject> ActiveObjects = new List<GameObject>();
    private void Awake()
    {
        foreach (var item in passiveButtons)
        {
            item.InitializeComponent();
        }
    }
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
        //G101
        string valueID = _dataManager.GetStringValue($"{selectGameObject.name}_Text_Explain");
        //공격력+{0%}
        string valueText = _dataManager.GetString($"{selectGameObject.name}_Text_Explain");

        var passive_Value = _dataManager.GetData(valueID) as Passive_Value;
        char lastChar = selectGameObject.name[selectGameObject.name.Length - 1];

        int passiveIndex = (valueID[valueID.Length - 1] - '1');
        int lastidx = _player.SavePlayerData.passiveIndex[passiveIndex];

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
                Debug.Log("err");
                //해금된 자원 이미지 비활성화
                if (lastidx < 1)
                {
                    Reinforce_Need_Image.SetActive(true);
                    Reinforce_Need.gameObject.SetActive(true);
                }
                else
                {
                    Reinforce_Need_Image.SetActive(false);
                    Reinforce_Need.gameObject.SetActive(false);
                }

                break;
            case '3':
                Reinforce_Description.text = string.Format(valueText, passive_Value.Status_UP[2]);
                Reinforce_Need.text = passive_Value.Status_2to3_NeedResource.ToString();
                //해금된 자원 이미지 비활성화
                if (lastidx < 2)
                {
                    Reinforce_Need_Image.SetActive(true);
                    Reinforce_Need.gameObject.SetActive(true);
                }
                else
                {
                    Reinforce_Need_Image.SetActive(false);
                    Reinforce_Need.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void RenewAll()
    {
        ShopUIRenew();

        foreach (var item in passiveButtons)
        {
            item.RenewPsvBtn();
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
                ActivePassives_Text[i].gameObject.SetActive(true);
                ActivePassives_Text[i].text = ActiveObjects[i].name[ActiveObjects[i].name.Length - 1].ToString();
            }
            else
            {
                ActivePassives_Img[i].sprite = Frame_Sprite;
                ActivePassives_Img[i].color = Color.gray;
                ActivePassives_Text[i].gameObject.SetActive(false);
            }
        }

        Money_Text.text = _player.SavePlayerData.money.ToString();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        DeActiveShopUI();
    }

    public void DeActiveShopUI()
    {
        UIEvent.DeActiveShopUI();
    }

    public bool UnlockNextButton(GameObject unlockObject)
    {
        string valueID = _dataManager.GetStringValue($"{unlockObject.name}_Text_Explain");
        var passive_Value = _dataManager.GetData(valueID) as Passive_Value;
        char lastChar = unlockObject.name[unlockObject.name.Length - 1];
        int lastidx = lastChar - '1';

        int needMoney = lastidx == 1 ? passive_Value.Status_1to2_NeedResource : passive_Value.Status_2to3_NeedResource;
        if(_player.SavePlayerData.money < needMoney)
        {
            return false;
        }

        _player.SavePlayerData.money -= needMoney;
        int passiveIndex = (valueID[valueID.Length - 1] - '1');
        _player.SavePlayerData.passiveIndex[passiveIndex] = lastidx;

        //활성화 되어있던 패시브 제거
        string previous_Passive_Name = $"{unlockObject.name.Substring(0, unlockObject.name.Length - 1)}{lastidx}";
        int index = 0;
        foreach(var activeObejct in ActiveObjects)
        {
            if(previous_Passive_Name.Equals(activeObejct.name))
            {
                OnSubmit_RemoveActiveObject(index);
                break;
            }
            index++;
        }

        RenewPanel(currentGameObject);

        //업적 호출
        _player.OnCalled_Achieve_AllUnlockPassive();
        return true;
    }


    public bool AddActiveObject(GameObject addGameObject)
    {
        //최대치
        if(ActiveObjects.Count >= 3) { return false; }

        // 중복 차단
        if(ActiveObjects.Contains(addGameObject)) { return false; }

        ActiveObjects.Add(addGameObject);

        string valueID = _dataManager.GetStringValue($"{addGameObject.name}_Text_Explain");
        var passive_Value = _dataManager.GetData(valueID) as Passive_Value;
        char lastChar = addGameObject.name[addGameObject.name.Length - 1];
        int lastidx = lastChar - '1';
        switch(valueID)
        {
            case "G101":
                _player.PlayerPassiveData.BAttack += passive_Value.Status_UP[lastidx] / 100f;
                break;
            case "G102":
                _player.PlayerPassiveData.AddHP += passive_Value.Status_UP[lastidx] / 100f;
                break;
            case "G103":
                _player.PlayerPassiveData.AddMove += passive_Value.Status_UP[lastidx] / 100f;
                break;
            case "G104":
                _player.PlayerPassiveData.AddOwnNum += (int)passive_Value.Status_UP[lastidx];
                break;
            case "G105":
                _player.PlayerPassiveData.AddStaRecovery += (int)passive_Value.Status_UP[lastidx];
                break;
            case "G106":
                _player.PlayerPassiveData.EAttack += passive_Value.Status_UP[lastidx] / 100f;
                break;
        }

        ShopUIRenew();
        return true;
    }
    public void OnSubmit_RemoveActiveObject(int index)
    {
        if (index > ActiveObjects.Count - 1) return;
        var activeObject = ActiveObjects[index];
        ActiveObjects.Remove(activeObject);
        var psvBtn = activeObject.GetComponent<PassiveButton>();
        psvBtn.DeActiveFrame();

        string valueID = _dataManager.GetStringValue($"{activeObject.name}_Text_Explain");
        var passive_Value = _dataManager.GetData(valueID) as Passive_Value;
        char lastChar = activeObject.name[activeObject.name.Length - 1];
        int lastidx = lastChar - '1';
        switch (valueID)
        {
            case "G101":
                _player.PlayerPassiveData.BAttack -= passive_Value.Status_UP[lastidx] / 100f;
                break;
            case "G102":
                _player.PlayerPassiveData.AddHP -= passive_Value.Status_UP[lastidx] / 100f;
                break;
            case "G103":
                _player.PlayerPassiveData.AddMove -= passive_Value.Status_UP[lastidx] / 100f;
                break;
            case "G104":
                _player.PlayerPassiveData.AddOwnNum -= (int)passive_Value.Status_UP[lastidx];
                break;
            case "G105":
                _player.PlayerPassiveData.AddStaRecovery -= (int)passive_Value.Status_UP[lastidx];
                break;
            case "G106":
                _player.PlayerPassiveData.EAttack -= passive_Value.Status_UP[lastidx] / 100f;
                break;
        }

        ShopUIRenew();
    }
}
