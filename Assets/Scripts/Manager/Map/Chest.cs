using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour, IInteractable
{
    [Inject] UIEvent UIEvent;
    public void Interact()
    {
        UIEvent.ActiveBlueChipUI();
    }
}
