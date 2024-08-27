using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UICanvas : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [SerializeField] BlueChipUI BlueChipUI;

    public void Awake()
    {
        UIEvent.RegisterBlueChipUI(BlueChipUI);
    }
}
