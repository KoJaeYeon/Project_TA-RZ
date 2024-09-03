using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour, IInteractable
{
    [Inject] UIEvent UIEvent;

    public string GetText()
    {
        return "암 유닛 강화";
    }

    public void Interact()
    {
        UIEvent.ActiveShopUI();
    }
}