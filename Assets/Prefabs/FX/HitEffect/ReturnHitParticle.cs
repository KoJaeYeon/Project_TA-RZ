using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ReturnHitParticle : MonoBehaviour
{

    public IEnumerator ReturnParticle(GameObject hitParticle, PoolManager pool)
    {
        ParticleSystem particleSystem = hitParticle.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            yield return new WaitWhile(() => particleSystem.isPlaying);
        }

        pool.EnqueueObject(hitParticle);
    }
}
