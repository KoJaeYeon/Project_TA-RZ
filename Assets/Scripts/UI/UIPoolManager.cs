using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPoolManager : MonoBehaviour
{
    [Inject] PoolManager _poolManager;
    [SerializeField] GameObject DamageText;
    private void Awake()
    {
        _poolManager.CreatePool(DamageText);
    }
}
