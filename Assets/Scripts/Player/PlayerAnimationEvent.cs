using UnityEngine;
using Zenject;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Inject]
    private Player _player;

    
    public void NextCombo()
    {
        _player.IsNext = true;
    }

}
