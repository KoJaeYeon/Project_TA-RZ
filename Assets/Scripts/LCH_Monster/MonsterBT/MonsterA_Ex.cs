using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class MonsterA_Ex : MonoBehaviour
{
    [Inject] Monster Monster;
    [Inject] Player player;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.Hit(Monster.Mon_Common_Damage, 0, transform);
        }
    }
}
