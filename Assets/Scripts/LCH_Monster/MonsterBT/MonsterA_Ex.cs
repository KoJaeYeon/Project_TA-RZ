using System.Collections;
using UnityEngine;

public class MonsterA_Ex : MonoBehaviour
{
    private Monster_A monsterA;
    private bool isDamaged;
    private SphereCollider collider;
    [SerializeField] private Transform _parentTrans;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        StartCoroutine(OffCollider());
        collider.radius = monsterA.TargetScale.x / 2;
    }
    private void OnDisable()
    {
        collider.enabled = true;
    }
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

    
    private IEnumerator OffCollider()
    {
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;
        yield return new WaitForSeconds(2.9f);
        transform.SetParent(_parentTrans);
        gameObject.SetActive(false);
    }
}
