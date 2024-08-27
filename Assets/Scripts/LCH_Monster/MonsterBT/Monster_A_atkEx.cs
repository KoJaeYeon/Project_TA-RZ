using System.Collections;
using UnityEngine;
using Zenject;

public class Monster_A_atkEx : MonoBehaviour
{
    private float growDuration = 2.0f;  
    private Vector3 targetScale = new Vector3(1f, 0.1f, 1f);
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
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / growDuration);
            timeElapsed += Time.deltaTime;
            
            yield return null;  
        }
       // poolManager.DequeueObject(explosionPrefab);
        if(transform.localScale == targetScale)
        {
            yield return null;
        }
    }

    
}
