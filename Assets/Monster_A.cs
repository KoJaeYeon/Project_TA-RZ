using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_A : Monster
{
    [SerializeField] GameObject atkPrefab;

    public void StartAtk()
    {
       PoolManager.Instantiate(atkPrefab);


    }

    private IEnumerator WaitAtk(float coolTime)
    {
        
        yield return new WaitForSeconds(coolTime);
    }
}
