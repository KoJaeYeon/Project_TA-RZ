using UnityEngine;
using Zenject;

public class PlayerInteractionTrigger : MonoBehaviour
{
    [Inject]
    Player _player;
    [Inject] UIEvent _UIEvent;
    private void OnTriggerEnter(Collider other)
    {
        var iInter = other.GetComponent<IInteractable>();
        if(iInter == null )
        {
            return;
        }

        _player.Interactable = iInter;
        string interactName = iInter.GetText();
        _UIEvent.ActiveInteractUI(interactName);
    }

    void OnTriggerExit(Collider other)
    {
        var iInter = other.GetComponent<IInteractable>();
        if (iInter == null)
        {
            return;
        }
        else if(iInter == _player.Interactable)
        {
            _UIEvent.DeActiveInteractUI();
        }
    }
}
