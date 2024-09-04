using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class OutGameUI : MonoBehaviour
{
    [Inject] SaveManager _saveManager;

    [SerializeField] InputActionReference cancelAction;
    [SerializeField] GameObject[] ChoiceBox;

    [SerializeField] TextMeshProUGUI[] continueText;
    [SerializeField] TextMeshProUGUI[] newText;

    void OnEnable()
    {
        cancelAction.action.Enable();
        cancelAction.action.performed += OnCancel;

        
        SaveDataRenew();
    }

    void OnDisable()
    {
        cancelAction.action.performed -= OnCancel;
        cancelAction.action.Disable();
    }

    private void SaveDataRenew()
    {
        for(int i = 0; i < 3; i++)
        {
            var load_Data = _saveManager.Load(i);

            string date = load_Data == null? "Empty" : load_Data.saveTime;
            continueText[i].text = date;
            newText[i].text = date;
        }
        
    }


    public void OnSublit_LoadGame(int index)
    {
        if (continueText[index].text.Equals("Empty")) return;
        _saveManager.saveIndex = index;
        SceneManager.LoadScene(1);
    }

    public void OnSubmit_NewGame(int index)
    {
        _saveManager.saveIndex = index;
        _saveManager.Save(new Save_PlayerData());
        SceneManager.LoadScene(1);
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
