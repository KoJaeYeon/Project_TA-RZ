using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OutGameUI : MonoBehaviour
{
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] GameObject[] ChoiceBox;

    void OnEnable()
    {
        cancelAction.action.Enable();
        cancelAction.action.performed += OnCancel;
    }

    void OnDisable()
    {
        cancelAction.action.performed -= OnCancel;
        cancelAction.action.Disable();
    }

    public void OnSublit_LoadGame(int index)
    {
        SceneManager.LoadScene(0);
    }

    public void OnSubmit_NewGame(int index)
    {
        SceneManager.LoadScene(0);
    }

    public void OnSubmit_ContinueBox()
    {
        ChoiceBox[0].SetActive(false);
        ChoiceBox[1].SetActive(true);
    }
    public void OnSubmit_NewGameBox()
    {
        ChoiceBox[0].SetActive(false);
        ChoiceBox[2].SetActive(true);
    }

    public void OnSubmit_Quit()
    {
        Application.Quit();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log("Cancel");
        if (ChoiceBox[1].activeSelf)
        {
            // 메뉴가 열려 있으면 닫기
            ChoiceBox[1].SetActive(false);
            ChoiceBox[0].SetActive(true);
        }
        else if (ChoiceBox[2].activeSelf)
        {
            ChoiceBox[2].SetActive(false);
            ChoiceBox[0].SetActive(true);
        }
    }
}
