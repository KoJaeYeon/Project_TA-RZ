using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using System.ComponentModel;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    [Header("Normal")]
    [SerializeField] private GameObject _normalStageUI;

    [Header("Boss")]
    [SerializeField] private GameObject _bossStageUI;

    [Header("ProgressUI")]
    [SerializeField] private Image _progressImage;

    [Inject] private UIEvent _uiEvent;
    [Inject] private MapManager _mapManager;
    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    #region Progress
    private Image _progressBar;
    private ProgressUIViewModel _progressView;
    private DiContainer _container;
    private float _currentProgressvalue;
    #endregion

    #region ChoiceUI
    private List<GameObject> _normalList;
    private List<GameObject> _bossList;
    private GameObject _currentUI;
    private StageType _currentStage;
    private bool _isChoice;
    #endregion

    private void OnEnable()
    {
        InitializeProgressUIOnEnable();
        InitializeChoiceUIOnEnable();
    }

    private void Start()
    {
        InitialzeProgressUIStart();
        InitializeChoiceUIStart();
    }

    #region Initialize
    private void InitializeProgressUIOnEnable()
    {
        if (_progressView == null)
        {
            _progressView =  _container.Instantiate<ProgressUIViewModel>();

            _progressView.PropertyChanged += ChangeProgressBar;

            _progressView.RegisterChangeProgressUIOnEnable();
        }
    }

    private void InitialzeProgressUIStart()
    {
        _progressBar = _progressImage;

        _progressBar.fillAmount = _mapManager.ProgressValue;
    }

    private void InitializeChoiceUIOnEnable()
    {
        _normalList = new List<GameObject>();

        foreach (Transform normalChild in _normalStageUI.transform)
        {
            if (_normalStageUI != null)
            {
                normalChild.gameObject.SetActive(false);
                _normalList.Add(normalChild.gameObject);
            }
        }

        _bossList = new List<GameObject>();

        foreach (Transform bossChild in _bossStageUI.transform)
        {
            if (_bossStageUI != null)
            {
                bossChild.gameObject.SetActive(false);
                _bossList.Add(bossChild.gameObject);
            }
        }
    }

    private void InitializeChoiceUIStart()
    {
        _uiEvent.AddEventChoiceStageEvent(true, SetStage);
    }
    #endregion

    private void ChangeProgressBar(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(_progressView.CurrentProgress))
        {
            _progressBar.fillAmount = _progressView.CurrentProgress;
        }
    }

    public void SetStage(Player player)
    {
        _isChoice = false;

        StartCoroutine(ChoiceStage(player));
    }

    private IEnumerator ChoiceStage(Player player)
    {
        PlayerInput input = player.GetComponent<PlayerInput>();

        input.enabled = false;

        if (_currentProgressvalue < 0.66f)
        {
            _currentUI = RandomUI();
        }
        else
        {
            _currentUI = BossUI();
        }
        
        _currentUI.SetActive(true);

        yield return new WaitWhile(() => !_isChoice);

        _currentUI.SetActive(false);

        _uiEvent.RequestChangeProgressBar(0.33f);

        _mapManager.SetStage(_currentStage, player, _currentProgressvalue);
    }

    private GameObject BossUI()
    {
        GameObject bossUI = _bossList[0];

        _bossList.RemoveAt(0);

        return bossUI;
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
