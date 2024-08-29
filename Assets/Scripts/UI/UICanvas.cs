using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UICanvas : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [SerializeField] BlueChipUI BlueChipUI;
    [SerializeField] GameUI GameUI;
    [SerializeField] LoadingUI _loadUI;

    public void Awake()
    {
        UIEvent.RegisterBlueChipUI(BlueChipUI);
        UIEvent.RegisterGameUI(GameUI);
        UIEvent.RegisterLoadUI(_loadUI);
    }
}
