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

    [Header("BlockingImage")]
    [SerializeField] private GameObject _blockingImage;

    [Header("ProgressUI")]
    [SerializeField] private GameObject _progressUI;
    [SerializeField] private Image _progressImage;

    [SerializeField] InputActionReference cancelAction;

    [Inject] private UIEvent _uiEvent;    
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
    [SerializeField] List<GameObject> _normalList;
    private List<GameObject> _bossList;
    private GameObject _currentUI;
    private StageType _currentStage;
    private bool _isChoice;
    #endregion

    #region LoadingUI

    #endregion

    private void OnEnable()
    {
        _blockingImage.SetActive(true);

        InitializeProgressUIOnEnable();
        InitializeChoiceUIOnEnable();

        _currentProgressvalue = _uiEvent.GetProgressValue();
        _uiEvent.RequestChangeProgressBar(_currentProgressvalue);

        ActiveChoiceUI();
        _uiEvent.SetActivePlayerControl(false);
        cancelAction.action.performed += OnCancel;
    }

    private void OnDisable()
    {
        cancelAction.action.performed -= OnCancel;
        _blockingImage.SetActive(false);
    }

    #region Initialize
    private void InitializeProgressUIOnEnable()
    {
        if (_progressView == null)
        {
            _progressView =  _container.Instantiate<ProgressUIViewModel>();

            _progressView.PropertyChanged += ChangeProgressBar;

            _progressView.RegisterChangeProgressUIOnEnable();

            _progressBar = _progressImage;

            _progressBar.fillAmount = 0;
        }
    }

    private void InitializeChoiceUIOnEnable()
    {        
        if(_bossList == null)
        {
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
    }

    #endregion

    private void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log("cancel");
        gameObject.SetActive(false);
        _uiEvent.SetActivePlayerControl(true);
    }

    private void ChangeProgressBar(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(_progressView.CurrentProgress))
        {
            _currentProgressvalue = _progressView.CurrentProgress;
            
            _progressBar.fillAmount = _progressView.CurrentProgress;
        }
    }

    private void ActiveChoiceUI()
    {
        _isChoice = false;

        StartCoroutine(ChoiceStage());
    }

    private IEnumerator ChoiceStage()
    {
        //활성화된 선택지가 없는 경우
        if (_currentUI == null)
        {
            if (_currentProgressvalue <= 0.99f)
            {
                _currentUI = RandomUI();
            }
            else
            {
                _currentUI = BossUI();
            }
        }

        ActiveUI(true);

        yield return new WaitWhile(() => !_isChoice);

        ActiveUI(false);

        _uiEvent.RequestChangeStage(_currentStage);

        _currentUI = null;

        this.gameObject.SetActive(false);
    }

    private void ActiveUI(bool active)
    {
        _progressUI.SetActive(active);

        _currentUI.SetActive(active);        
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
