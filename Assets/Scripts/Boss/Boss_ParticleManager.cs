using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Boss_ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle_Gimmick;

    [SerializeField] private ParticleSystem particle_GimmickExplosion;

    [SerializeField] private ParticleSystem particle_RootAttack;

    [SerializeField] private ParticleSystem particle_Smash;

    [SerializeField] private ParticleSystem particle_Explosion;

    private IObjectPool<RootSmashPool> _smashPool;
    private IObjectPool<ExplosionParticlePool> _explosionPool;

    private void Awake()
    {
        _smashPool = new ObjectPool<RootSmashPool>(CreateParticle_Smash, OnGetParticle_Smash, OnReleaseParticle_Smash, OnDestroyParticle_Smash);
        _explosionPool = new ObjectPool<ExplosionParticlePool>(CreateParticle_Explosion, OnGetParticle_Explosion, OnReleaseParticle_Explosion, OnDestroyParticle_Explosion);
    }

    public void OnGimmickCharging(Transform tr)
    {
        particle_Gimmick.transform.position = tr.position;
        particle_Gimmick.Play();
    }
    public void OnGimmickExplosion(Transform tr)
    { 
        particle_Gimmick.Stop();
        particle_GimmickExplosion.transform.position = tr.position;
        particle_GimmickExplosion.Play();
    }
    public void OnGimmickCancle()
    {
        particle_Gimmick.Stop();
    }

    public void OnRootAttack(Transform tr)
    { 
        particle_RootAttack.transform.position = tr.position;
        particle_RootAttack.transform.rotation = tr.rotation;
        particle_RootAttack.Play();
    }

    public void OnSmash(Transform[] tr)
    {
        foreach (var t in tr)
        {
            var particle = _smashPool.Get();
            particle.transform.position = t.position;

            particle.ParticlePlay();
        }
    }

    public void OnFirstExplosion(Transform[] tr)
    {
        foreach (var t in tr)
        {
            var particle = _explosionPool.Get();
            particle.transform.position = t.position;

            particle.ParticlePlay();
        }
    }

    public void OnSecondExplosion(Transform[] tr)
    {
        foreach (var t in tr)
        {
            var particle = _explosionPool.Get();
            particle.transform.position = t.position;

            particle.ParticlePlay();
        }
    }

    private RootSmashPool CreateParticle_Smash()
    {
        RootSmashPool particle = Instantiate(particle_Smash, transform).GetComponent<RootSmashPool>();
        particle.SetManagedPool(_smashPool);
        return particle;
    }
    private void OnGetParticle_Smash(RootSmashPool particle)
    {
        particle.gameObject.SetActive(true);
    }
    private void OnReleaseParticle_Smash(RootSmashPool particle)
    {
        particle.gameObject.SetActive(false);
    }
    private void OnDestroyParticle_Smash(RootSmashPool particle)
    {
        Destroy(particle.gameObject);
    }

    private ExplosionParticlePool CreateParticle_Explosion()
    {
        ExplosionParticlePool particle = Instantiate(particle_Explosion, transform).GetComponent<ExplosionParticlePool>();
        particle.SetManagedPool(_explosionPool);
        return particle;
    }
    private void OnGetParticle_Explosion(ExplosionParticlePool particle)
    {
        particle.gameObject.SetActive(true);
    }
    private void OnReleaseParticle_Explosion(ExplosionParticlePool particle)
    {
        particle.gameObject.SetActive(false);
    }
    private void OnDestroyParticle_Explosion(ExplosionParticlePool particle)
    {
        Destroy(particle.gameObject);
    }
}
