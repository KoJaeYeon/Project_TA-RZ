using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ParticleContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        if (FindObjectOfType<Boss_ParticleManager>() != null)
        {
            Container.Bind<Boss_ParticleManager>().FromComponentInHierarchy().AsSingle();
        }
        else
        {
            Debug.LogWarning("Boss Controller가 없습니다. 바인딩 되지 않습니다.");
        }
    }
}
