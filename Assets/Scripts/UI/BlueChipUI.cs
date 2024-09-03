using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlueChipUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    private void OnEnable()
    {
        UIEvent.SetActivePlayerControl(false);
    }

    private void OnDisable()
    {
        UIEvent.SetActivePlayerControl(true);
    }

    public void DeActiveBlueChipUI()
    {
        UIEvent.DeActiveBlueChipUI();
    }
}
