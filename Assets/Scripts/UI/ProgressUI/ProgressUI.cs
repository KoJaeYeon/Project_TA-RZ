using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProgressUI : MonoBehaviour
{
    [Header("ProgressUI")]
    [SerializeField] private GameObject _progressObject;

    private Image _progressBar;
    private ProgressUIViewModel _progressView;
    private DiContainer _container;

    private float _currentValue;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void OnEnable()
    {
        if(_progressView == null)
        {
            _progressView = _container.Instantiate<ProgressUIViewModel>();

            _progressView.PropertyChanged += ChangeProgressBar;

            _progressView.RegisterChangeProgressUIOnEnable();
        }
    }

    private void Start()
    {
        InitializeProgressUI();
    }

    private void InitializeProgressUI()
    {
        _currentValue = 0f;

        _progressBar = _progressObject.transform.GetChild(0).GetComponent<Image>();

        _progressBar.fillAmount = _currentValue;
    }

    private void ChangeProgressBar(object sender, PropertyChangedEventArgs args)
    {
        if(args.PropertyName == nameof(_progressView.CurrentProgress))
        {
            _progressBar.fillAmount = _progressView.CurrentProgress;
        }
    }
}


