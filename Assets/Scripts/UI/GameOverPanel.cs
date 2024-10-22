using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        if(_restartButton != null)
        {
            EventSystem.current.SetSelectedGameObject(_restartButton.gameObject);
        }
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Project", LoadSceneMode.Single);
    }
}
