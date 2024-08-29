using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using UnityEngine;
using Zenject;

public class Monster_A_atkEx : MonoBehaviour
{
    private float growDuration = 2.0f;
    [SerializeField] GameObject target;

    private Vector3 targetScale;

    private void Start()
    {
        targetScale = target.transform.localScale;
    }

    private void OnEnable()
    {
        targetScale = target.transform.localScale;

        transform.SetParent(null);

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
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / growDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localScale = target.transform.localScale;
        transform.SetParent(target.transform);

        if (transform.localScale == targetScale)
        {
            gameObject.SetActive(false);
            yield return null;
        }
    }
}
