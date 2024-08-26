using System.Collections;
using UnityEngine;
using Zenject;

public class Monster_A_atkEx : MonoBehaviour
{
    private float growDuration = 2.0f;  
    private Vector3 targetScale = new Vector3(2f, 0.1f, 2f);

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        StartCoroutine(GrowOverTime());
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
    }
}
