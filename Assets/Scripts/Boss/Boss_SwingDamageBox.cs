using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Boss_SwingDamageBox : MonoBehaviour
{
    [SerializeField] private float _angle;

    [SerializeField] BossController _boss;

    [Inject] Player _player { get; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 forward = transform.forward;

            Vector3 direction = (other.transform.position - transform.position).normalized;

            float angle = Vector3.Angle(forward, direction);

            if(angle <= _angle) 
            {
                _player.Hit(_boss.explosionDamage, 1f, _boss.transform);
            }
        }
    }
}
