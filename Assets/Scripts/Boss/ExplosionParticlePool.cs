using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExplosionParticlePool : MonoBehaviour
{
    private ParticleSystem _ps;

    private IObjectPool<ExplosionParticlePool> _pool;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    public void ParticlePlay()
    {
        _ps.Play();
    }

    public void SetManagedPool(IObjectPool<ExplosionParticlePool> pool)
    {
        _pool = pool;
    }
}
