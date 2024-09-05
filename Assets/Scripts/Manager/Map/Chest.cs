using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour, IInteractable
{
    [Inject] UIEvent UIEvent;
    [SerializeField] GameObject Portal;
    public void Interact()
    {
        UIEvent.ActiveBlueChipUI(this);
        UIEvent.DeactiveQuestUI();
    }

    public string GetText()
    {
        return "블루칩 획득";
    }

    public void SetActivePoratal()
    {
        Portal.SetActive(true);
    }
}
