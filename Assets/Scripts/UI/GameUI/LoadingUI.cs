using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Image _loadImage;

    //private IEnumerator ImageBlink()
    //{
    //    float duration = 0.3f;
    //    Color color = _loadImage.color;

    //    //float totalBlinkTime = 5f;
    //    float elapsedBlinkTime = 0f;

    //    while (_isLoad == false)
    //    {
    //        yield return StartCoroutine(FadeImage(color, 1f, duration));

    //        yield return StartCoroutine(FadeImage(color, 0f, duration));

    //        elapsedBlinkTime += duration * 2;
    //    }

    //    yield return StartCoroutine(FadeImage(color, 1f, duration));

    //    yield return StartCoroutine(FadeImage(color, 0f, duration));

    //    SceneManager.UnloadSceneAsync("LoadingScene");
    //}

    private IEnumerator FadeImage(Color color, float targetAlpa, float duration)
    {
        float startAlpa = color.a;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpa, targetAlpa, elapsed / duration);
            _loadImage.color = color;
            yield return null;
        }

        color.a = targetAlpa;
        _loadImage.color = color;
    }
}
