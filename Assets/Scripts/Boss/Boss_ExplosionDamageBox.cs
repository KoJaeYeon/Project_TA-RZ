using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Boss_ExplosionDamageBox : MonoBehaviour
{
    [SerializeField] private float _attackDistance;

    [Inject] BossController _boss { get; }

    [Inject] Player _player { get; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance >= _attackDistance)
            {
                Debug.Log(this.gameObject.name + other.gameObject.name);
                _player.Hit(_boss.explosionDamage, 1f, _boss.transform);
            }
        }
    }
}
