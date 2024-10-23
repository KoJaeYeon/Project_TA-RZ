using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using UnityEngine;
using Zenject;

public class Monster_A_atkEx : MonoBehaviour
{
    private float growDuration = 2.0f;
    [SerializeField] GameObject target;
    [SerializeField] Monster_A monsterA;

    private void Awake()
    {
        growDuration = monsterA.GrowDuration;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(GrowOverTime());
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }

    private IEnumerator GrowOverTime()
    {
        Vector3 initialScale = transform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < growDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, Vector3.one, timeElapsed / growDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        yield break;

    }
}
