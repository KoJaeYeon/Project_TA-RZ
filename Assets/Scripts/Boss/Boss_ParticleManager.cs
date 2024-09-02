using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ParticleManager : MonoBehaviour
{
    public ParticleSystem particle_Gimmick;

    public ParticleSystem particle_GimmickExplosion;

    public ParticleSystem particle_RootAttack;

    public ParticleSystem particle_Smash;

    public ParticleSystem particle_Explosion;

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

    public void OnRootAttack(Transform tr)
    { 
        particle_RootAttack.transform.position = tr.position;
        particle_RootAttack.transform.rotation = tr.rotation;
        particle_RootAttack.Play();
    }

    public void OnSmash(Transform tr)
    {
        particle_Smash.transform.position = tr.position;
        particle_Smash.transform.rotation = tr.rotation;
        particle_Smash.Play();
    }

    public void OnExplosion()
    {

    }
}
