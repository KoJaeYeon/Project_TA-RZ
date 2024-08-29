using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Monster_C_Atk : MonoBehaviour
{
    private Monster_C monsterC;
    private bool isDamaged;
    private SphereCollider collider;

    private void OnEnable()
    {
        StartCoroutine(OffCollider());
    }
    private void OnDisable()
    {
        collider.enabled = true;
    }
    public void Initialize(Monster_C monster_C)
    {
        this.monsterC = monster_C;
        collider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // 'Player'가 'IHit' 인터페이스를 구현하는지 확인
        if (other.CompareTag("Player") == false)
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
        hitable.Hit(monsterC.Mon_Common_Damage, 0, monsterC.transform);
    }


    private IEnumerator OffCollider()
    {
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;
    }
}
