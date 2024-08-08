using UnityEngine;

public class PlayerManager
{
    private GameObject _player;


    public GameObject Player { get; private set; }



    public void SetPlayerObject(GameObject player)
    {
        if(player == null)
        {
            return;
        }

        _player = player;
    }
}
