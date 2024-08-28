using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BossContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BossController>().FromComponentInHierarchy().AsSingle();

        Container.Bind<Boss_MarkManager>().FromComponentInHierarchy().AsSingle();

        Container.Bind<Boss_DamageBoxManager>().FromComponentInHierarchy().AsSingle();
    }
}
