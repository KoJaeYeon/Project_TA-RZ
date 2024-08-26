using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Monster_A : Monster,IHit
{
    [SerializeField] private GameObject atkPrefab;
    [Inject] private PoolManager _poolManager;

    private void Awake()
    {
        _poolManager.CreatePool(atkPrefab);
        StartCoroutine(Ex());
    }

    public void StartAtk()
    {
        Vector3 playerPosition = Player.transform.position;
        GameObject atkObject = _poolManager.DequeueObject(atkPrefab);
        atkObject.transform.position = playerPosition;
    }

    IEnumerator Ex()
    {
        Mon_Common_CoolTime = 2.0f;
        Mon_Common_Range = 10.0f;
        yield return new WaitForSeconds(3f);
        
    }
}
