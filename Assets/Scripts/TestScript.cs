using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestScript : MonoBehaviour, IHit
{
    // Start is called before the first frame update
    // 한글테스트
    [Inject] Player player;

    public void ApplyKnockback(Vector3 otherPosition, float knockBackTime)
    {
        
    }

    public void Hit(float damage, float paralysisTime, Transform attackTrans)
    {
        Debug.Log(damage);
        Debug.Log(paralysisTime);
        Debug.Log(attackTrans);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            player.ApplyKnockback(transform.position, 1f);
            player.Hit(10,0.4f,transform);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            player.Hit(10,0.4f, transform);
        }
    }
}
