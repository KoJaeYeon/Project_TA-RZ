using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BossContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        if (FindObjectOfType<BossController>() != null)
        {
            Container.Bind<BossController>().FromComponentInHierarchy().AsSingle();

            Container.Bind<Boss_MarkManager>().FromComponentInHierarchy().AsSingle();

            Container.Bind<Boss_DamageBoxManager>().FromComponentInHierarchy().AsSingle();
        }
        else
        {
            Debug.LogWarning("Boss Controller가 없습니다. 바인딩 되지 않습니다.");
        }
    }
}
