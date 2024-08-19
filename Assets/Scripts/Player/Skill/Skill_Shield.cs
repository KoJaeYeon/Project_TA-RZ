using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shield : MonoBehaviour
{
    bool isActive;
    float value;
    BoxCollider boxCollider;
    Vector3 initSize;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        initSize = boxCollider.size;
    }

    private void OnEnable()
    {
        isActive = false;
        boxCollider.isTrigger = false;
        boxCollider.size = initSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            var ihit = other.gameObject.GetComponent<IHit>();
            if (ihit != null)
            {
                ihit.ApplyKnockback(value, transform);
            }
        }
    }

    public void ApplyShield(float value)
    {
        isActive = true;
        this.value = value;
        boxCollider.isTrigger = true;
        boxCollider.size *= 1.1f;
    }
}
