using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using UnityEngine;
using Zenject;

public class Monster_C_AtkRange : MonoBehaviour
{
    private float growDuration = 2.0f;
    [SerializeField] GameObject target;

    private void OnEnable()
    {
        transform.SetParent(null);
        transform.localScale = Vector3.zero;
        StartCoroutine(GrowOverTime());
    }
    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
        transform.SetParent(target.transform);

    }

    private IEnumerator GrowOverTime()
    {
        transform.position = target.transform.position;
        Vector3 initialScale = transform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < growDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, target.transform.localScale, timeElapsed / growDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localScale = target.transform.localScale;
        transform.SetParent(target.transform);

        if (transform.localScale == target.transform.localScale)
        {
            gameObject.SetActive(false);
            yield return null;
        }
    }
}
