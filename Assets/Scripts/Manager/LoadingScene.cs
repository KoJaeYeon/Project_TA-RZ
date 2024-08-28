using BehaviorDesigner.Runtime;
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

        SceneManager.LoadScene("LoadingScene");
        var bm = FindObjectOfType<BehaviorManager>();
        if(bm != null)
        {
            DontDestroyOnLoad(bm);
        }
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
        StartCoroutine(ImageBlink());
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextScene, LoadSceneMode.Additive);

        while(true)
        {
            Debug.Log($"op : {op}, opDone:{op.isDone}, opProgress: {op.progress}");
            yield return null;

            if (op.isDone)
            {
                _isLoad = true;
                yield break;
            }

        }

    }

    private IEnumerator ImageBlink()
    {
        float duration = 0.3f;
        Color color = _loadImage.color;

        //float totalBlinkTime = 5f;
        float elapsedBlinkTime = 0f;

        while (_isLoad == false)
        {
            yield return StartCoroutine(FadeImage(color, 1f, duration));

            yield return StartCoroutine(FadeImage(color, 0f, duration));

            elapsedBlinkTime += duration * 2;
        }

        yield return StartCoroutine(FadeImage(color, 1f, duration));

        yield return StartCoroutine(FadeImage(color, 0f, duration));

        SceneManager.UnloadSceneAsync("LoadingScene");
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
