using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour, IInteractable
{
    [Inject] UIEvent UIEvent;
    [SerializeField] GameObject Portal;
    [SerializeField] MapType _mapType;
    public MapType MapType { get { return _mapType; } }
    public void Interact()
    {
        UIEvent.ActiveBlueChipUI(this);
        UIEvent.DeactiveQuestUI();
    }

    public string GetText()
    {
        return "보상 획득";
    }

    public void SetActivePoratal()
    {
        Portal.SetActive(true);
    }
}
