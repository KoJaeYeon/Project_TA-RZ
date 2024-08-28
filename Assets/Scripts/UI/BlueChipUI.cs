using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlueChipUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        UIEvent.SetActivePlayerControl(false);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIEvent.SetActivePlayerControl(true);
    }

    public void DeActiveBlueChipUI()
    {
        gameObject.SetActive(false);
    }
}
