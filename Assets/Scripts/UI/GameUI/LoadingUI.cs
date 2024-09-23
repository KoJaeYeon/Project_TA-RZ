using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingUI : MonoBehaviour
{
    [Inject]
    private UIEvent _uiEvent;
    [Inject]
    private Player _player;

    [SerializeField]
    private Image _loadImage;

    private void OnEnable()
    {
        _uiEvent.SetActivePlayerControl(false);
        
    }

    private void OnDisable()
    {
        _player.OnSave_PlayerData();
        _player.StartCoroutine(PlayerInputEnable());
    }

    IEnumerator PlayerInputEnable()
    {
        yield return new WaitForSeconds(1f);
        _uiEvent.SetActivePlayerControl(true);
    }

    public IEnumerator ImageBlink(ResourceRequest request)
    {
        float duration = 0.3f;
        Color color = _loadImage.color;

        while (!request.isDone)
        {
            yield return StartCoroutine(FadeImage(color, 1f, duration));

            yield return StartCoroutine(FadeImage(color, 0f, duration));
        }

        yield return StartCoroutine(FadeImage(color, 1f, duration));

        yield return StartCoroutine(FadeImage(color, 0f, duration));

        this.gameObject.SetActive(false);
    }

    private IEnumerator FadeImage(Color color, float targetAlpa, float duration)
    {
        float startAlpa = color.a;
        float elapsed = 0f;

        while (elapsed < duration)
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
