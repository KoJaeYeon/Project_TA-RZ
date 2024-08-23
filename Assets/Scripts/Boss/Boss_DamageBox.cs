using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_DamageBox : MonoBehaviour
{
    [SerializeField] private string asdf;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Debug.Log(other.gameObject.name + asdf);
    }
}
