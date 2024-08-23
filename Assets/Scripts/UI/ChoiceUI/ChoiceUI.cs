using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChoiceUI : MonoBehaviour
{
    [Header("Normal")]
    [SerializeField] private GameObject _normalStageUI;

    [Header("Boss")]
    [SerializeField] private GameObject _bossStageUI;

    [Inject] private UIEvent _uiEvent;
    [Inject] private MapManager _mapManager;

    private List<GameObject> _normalList;
    private List<GameObject> _bossList;

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
        _normalList = new List<GameObject>();

        foreach(Transform normalChild in _normalStageUI.transform)
        {
            if(_normalStageUI != null)
            {
                normalChild.gameObject.SetActive(false);
                _normalList.Add(normalChild.gameObject);
            }
        }

        _bossList = new List<GameObject>();

        foreach(Transform bossChild in _bossStageUI.transform)
        {
            if(_bossStageUI != null)
            {
                bossChild.gameObject.SetActive(false);
                _bossList.Add(bossChild.gameObject);
            }
        }
    }

    public void SetStage(GameObject playerObject)
    {
        _isChoice = false;

        StartCoroutine(ChoiceStage(playerObject));
    }

    private IEnumerator ChoiceStage(GameObject playerObject)
    {
        GameObject stageUI = RandomUI();
        
        stageUI.SetActive(true);

        yield return new WaitWhile(() => !_isChoice);

        stageUI.SetActive(false);

        _mapManager.SetStage(_currentStage, playerObject);
    }

    private GameObject RandomUI()
    {
        int randomValue = Random.Range(0, _normalList.Count);

        GameObject randomUIobject = _normalList[randomValue];

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
