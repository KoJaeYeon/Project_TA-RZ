using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestScript : MonoBehaviour, IHit
{
    // Start is called before the first frame update
    // 한글테스트
    [Inject] Dictionary<int, Stat> dic;
    [Inject] GoogleSheetManager sheetManager;
    PC_Common_Stat PC_Common_Stat;
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

    void Start()
    {
        //1안
        PC_Common_Stat = dic[101] as PC_Common_Stat;

        //2안
        //var stat = sheetManager.GetStat(id) as PC_Common_Stat;
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
