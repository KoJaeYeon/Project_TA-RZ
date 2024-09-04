using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RootSmashPool : MonoBehaviour
{
    private ParticleSystem _ps;

    private IObjectPool<RootSmashPool> _pool;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    public void ParticlePlay()
    {
        _ps.Play();
    }

    public void SetManagedPool(IObjectPool<RootSmashPool> pool)
    {
        _pool = pool;
    }
}
