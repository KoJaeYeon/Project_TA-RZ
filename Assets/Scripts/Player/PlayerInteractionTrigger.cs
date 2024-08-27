using UnityEngine;
using Zenject;

public class PlayerInteractionTrigger : MonoBehaviour
{
    [Inject]
    Player _player;
    private void OnTriggerEnter(Collider other)
    {
        var iInter = other.GetComponent<IInteractable>();
        if(iInter == null )
        {
            return;
        }

        _player.Interactable = iInter;
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
            _player.Interactable = null;
        }        
    }
}
