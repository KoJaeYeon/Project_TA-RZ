using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    // 한글테스트 
    [Inject] Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            player.ApplyKnockback(transform.position);
            player.Hit(10);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            player.Hit(10);
        }
    }
}
