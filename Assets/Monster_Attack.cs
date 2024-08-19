using UnityEngine;
using Zenject;
public class Monster_Attack : MonoBehaviour
{

    [Inject] Player Player;
    [SerializeField] Collider Collider;
    private void Awake()
    {
        Collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Hit(10, 2, transform);
            
        }
    }

    public void OnAtk()
    {
        Collider.enabled = true;
    }
    
}

