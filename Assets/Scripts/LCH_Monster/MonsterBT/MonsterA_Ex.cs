using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterA_Ex : MonoBehaviour
{
    private Monster_A monsterA;

    public void Initialize(Monster_A monsterA)
    {
        this.monsterA = monsterA;
    }
    private void OnTriggerEnter(Collider other)
    {
        // 'Player'가 'IHit' 인터페이스를 구현하는지 확인
        if(other.CompareTag("Player") == false)
        {
            return;
        }
        IHit hitable = other.GetComponent<IHit>();
        if (hitable == null)
        {
            Debug.LogError("Player does not implement IHit interface");
            return;
        }
        // Hit 메서드 호출
        hitable.Hit(monsterA.Mon_Common_Damage, 0, monsterA.transform);
    }
}
