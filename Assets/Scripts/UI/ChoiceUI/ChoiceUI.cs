using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChoiceUI : MonoBehaviour
{
    [Header("ChoiceUI_Triple")]
    [SerializeField] private GameObject _tripleUI;

    [Header("ChoiceUI_Dual_QE")]
    [SerializeField] private GameObject _dualQE;

    [Header("ChoiceUI_Dual_QN")]
    [SerializeField] private GameObject _dualQN;

    [Header("ChoiceUI_Dual_EN")]
    [SerializeField] private GameObject _dualEN;

    [Header("ChoiceUI_Boss")]
    [SerializeField] private GameObject _bossUI;

    [Inject] private UIEvent _uiEvent;

    private List<GameObject> _uiList;

    private StageType _currentStage;

    private bool _isChoice;

    private void OnEnable()
    {
        InitializeChoiceUI();
    }

    private void Start()
    {
        _uiEvent.AddEventChoiceStageEvent(true, SetStage);
    }

    private void InitializeChoiceUI()
    {
        _uiList = new List<GameObject>();   

        foreach(Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.SetActive(false);

            if(childTransform.gameObject != _bossUI)
            {
                _uiList.Add(childTransform.gameObject);
            }
        }
    }

    public StageType SetStage()
    {
        _isChoice = false;

        GameObject stageUI = RandomUI();

        stageUI.SetActive(true);

        StartCoroutine(IsChoice(stageUI));

        return _currentStage;
    }

    private IEnumerator IsChoice(GameObject stageUI)
    {
        yield return new WaitWhile(() => !_isChoice);

        stageUI.SetActive(false);
    }

    private GameObject RandomUI()
    {
        int randomValue = Random.Range(0, _uiList.Count);

        GameObject randomUIobject = _uiList[randomValue];

        return randomUIobject;
    }

    public void OnQuantityUIButtonClick()
    {
        _currentStage = StageType.Quantity;
        _isChoice = true;
    }

    public void OnEliteUIButtonClick()
    {
        _currentStage = StageType.Elite;
        _isChoice = true;
    }

    public void OnNormalUIButtonClick()
    {
        _currentStage = StageType.Normal;
        _isChoice = true;
    }

    public void OnBossUIButtonClick()
    {
        _currentStage = StageType.Boss;
        _isChoice = true;
    }
}
