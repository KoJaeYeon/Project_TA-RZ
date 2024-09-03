using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour, IInteractable
{
    [Inject] UIEvent UIEvent;
    public void Interact()
    {
        UIEvent.ActiveBlueChipUI();
    }

    public string GetText()
    {
        return "블루칩 획득";
    }
}
