using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    static string _nextScene;

    [SerializeField]
    private Image _loadImage;

    private bool _isLoad;

    private void OnEnable()
    {
        _isLoad = false;
    }

    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;

        SceneManager.LoadScene("LoadScene");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
        StartCoroutine(ImageBlink());
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextScene);

        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if(op.progress >= 0.9f && _isLoad)
            {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }

    private IEnumerator ImageBlink()
    {
        float duration = 0.3f;
        Color color = _loadImage.color;

        float totalBlinkTime = 5f;
        float elapsedBlinkTime = 0f;

        while(elapsedBlinkTime < totalBlinkTime)
        {
            yield return StartCoroutine(FadeImage(color, 1f, duration));

            yield return StartCoroutine(FadeImage(color, 0f, duration));

            elapsedBlinkTime += duration * 2;
        }

        _isLoad = true;

    }

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
