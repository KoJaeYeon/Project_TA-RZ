using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using UnityEngine;
using Zenject;

public class Monster_A_atkEx : MonoBehaviour
{
    private float growDuration = 2.0f;
    [SerializeField] GameObject target;

    [Header("타겟 범위 설정")]
    [SerializeField] private Vector3 targetScale;

    private void Start()
    {
        target.transform.localScale = targetScale;
       // targetScale = target.transform.localScale;
    }

    private void OnEnable()
    {

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

        transform.localScale = targetScale;
        transform.SetParent(target.transform);

        if (transform.localScale == targetScale)
        {
            gameObject.SetActive(false);
            yield return null;
        }
    }
}
