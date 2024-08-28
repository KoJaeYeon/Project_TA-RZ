using UnityEngine;
using Zenject;
public class Monster_Attack : MonoBehaviour
{

    Collider Collider;
    Monster _monster;
    private void Awake()
    {
        Collider = GetComponent<Collider>();
        _monster = GetComponentInParent<Monster>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _monster.Attack();            
        }
    }

    public void OnAtk()
    {
        Collider.enabled = true;
    }
    
}

