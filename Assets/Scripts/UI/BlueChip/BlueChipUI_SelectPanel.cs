using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlueChipUI_SelectPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _ui;

    public void OnFocusBlueChipBtn()
    {
        foreach (var ui in _ui)
        {
            if (ui.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(ui);
                return;
            }
        }
    }
}
