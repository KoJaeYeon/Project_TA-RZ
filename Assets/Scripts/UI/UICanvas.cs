using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UICanvas : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [SerializeField] BlueChipUI BlueChipUI;
    [SerializeField] GameUI GameUI;
    [SerializeField] LoadingUI LoadUI;
    [SerializeField] ShopUI ShopUI;
    [SerializeField] InteractUI InteractUI;

    public void Awake()
    {
        UIEvent.RegisterBlueChipUI(BlueChipUI);
        UIEvent.RegisterGameUI(GameUI);
        UIEvent.RegisterLoadUI(LoadUI);
        UIEvent.RegisterShopUI(ShopUI);
        UIEvent.RegisterInteractUI(InteractUI);
    }
}
