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
    }

    public void StartAtk()
    {
        Vector3 playerPosition = Player.transform.position;
        GameObject atkObject = _poolManager.DequeueObject(atkPrefab);
        atkObject.transform.position = playerPosition;
    }

    private IEnumerator WaitAtk(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
    }
}
