using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour, IInteractable
{
    [Inject] UIEvent UIEvent;
    public void Interact()
    {
        UIEvent.ActiveShopUI();
    }
}