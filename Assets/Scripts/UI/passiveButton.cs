using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveButton : MonoBehaviour
{
    [SerializeField] ShopUI shopUI;
    [SerializeField] GameObject Frame;
    Button btn;
    Image img;
    Save_PlayerData Save_PlayerData;

    int index;
    private void Awake()
    {
        btn = GetComponent<Button>();
        img = GetComponent<Image>();
        Save_PlayerData = shopUI._player.SavePlayerData;
    }

    public void RenewPsvBtn()
    {
        char lastChar = gameObject.name[gameObject.name.Length - 1];
        index = lastChar - '1';

        string passiveName = gameObject.name.Substring(0, gameObject.name.Length-1);
        int targetindex = -1;
        switch(passiveName)
        {
            case "UI_EternityReinforce_BAttackP":
                targetindex = 0;
                break;
            case "UI_EternityReinforce_ElementalAttackP":
                targetindex = 1;
                break;
            case "UI_EternityReinforce_Hp":
                targetindex = 2;
                break;
            case "UI_EternityReinforce_RunSpeed":
                targetindex = 3;
                break;
            case "UI_EternityReinforce_ResourceBag":
                targetindex = 4;
                break;
            case "UI_EternityReinforce_StaminaGainSpeed":
                targetindex = 5;
                break;
        }

        if (index < Save_PlayerData.passiveIndex[targetindex])
        {
            img.color = Color.gray;
            btn.onClick.RemoveAllListeners();            
        }
        else if (index == Save_PlayerData.passiveIndex[targetindex])
        {
            img.color = Color.red;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnSubmit_RequestAddPassive));            
        }
        else if (index == Save_PlayerData.passiveIndex[targetindex] + 1)
        {
            img.color = Color.white;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnSubmit_RequestUnlockPassive));
        }
        else
        {
            img.color = Color.white;
            btn.onClick.RemoveAllListeners();
        }
    }

    public void DeActiveFrame()
    {
        Frame.SetActive(false);
    }
    public void OnSubmit_RequestAddPassive()
    {
        bool succeed = shopUI.AddActiveObject(gameObject);
        if(succeed) Frame.SetActive(true);
    }
    public void OnSubmit_RequestUnlockPassive()
    {
        bool succeed = shopUI.UnlockNextButton(gameObject);
        if (succeed)
        {
            shopUI.RenewAll();
        }
    }
}
