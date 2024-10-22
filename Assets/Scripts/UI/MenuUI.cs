using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuUI : MonoBehaviour
{
    [Inject] UIEvent UIEvent;
    [SerializeField] InputActionReference cancelAction;
    [SerializeField]
    GameObject[] MenuUI_Child;
    [SerializeField] GameObject[] initial_Select_GameObject;
    int index = 0;

    public void OnEnable()
    {
        cancelAction.action.Enable();
        cancelAction.action.performed += OnCancel;
    }
    public void OnEnableMenuUI()
    {
        UIEvent.PlayerInfoActiveSelf();
        UIEvent.SetActivePlayerControl(false);
        UIEvent.SetActivePlayerUI(false);

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(index).gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(initial_Select_GameObject[index]);
    }

    private void OnDisableMenuUI()
    {
        UIEvent.SetActivePlayerControl(true);
        UIEvent.SetActivePlayerUI(true);
    }

    private void OnDisable()
    {
        cancelAction.action.performed -= OnCancel;
        cancelAction.action.Disable();
    }

    public void OutLobby()
    {
        index = 1;
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if(UIEvent._gameUI.gameObject.activeSelf)
        {
            return;
        }
        Debug.Log("cancel");
        if(MenuUI_Child[index].activeSelf)
        {
            Debug.Log("cancelif");
            Continue();
        }
        else if (MenuUI_Child[2].activeSelf)
        {
            ConfigOut();
        }
        else
        {
            Debug.Log("cancelelse");
            OnEnableMenuUI();
        }
    }

    public void Continue()
    {
        transform.GetChild(index).gameObject.SetActive(false);
        OnDisableMenuUI();
    }

    public void Config()
    {
        MenuUI_Child[2].SetActive(true);
        MenuUI_Child[index].SetActive(false);
        EventSystem.current.SetSelectedGameObject(initial_Select_GameObject[2]);
    }

    public void ConfigOut()
    {
        MenuUI_Child[2].SetActive(false);
        MenuUI_Child[index].SetActive(true);
        EventSystem.current.SetSelectedGameObject(initial_Select_GameObject[index]);
    }

    public void Lobby()
    {
        SceneManager.LoadScene("Project");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Title");
    }
}
