using UnityEngine;
using Zenject;

public class PassiveShop : MonoBehaviour, IInteractable
{
    [Inject] UIEvent UIEvent;

    public string GetText()
    {
        return "패시브 능력 강화";
    }

    public void Interact()
    {
        UIEvent.ActivePassiveShopUI();
    }
}