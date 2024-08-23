using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class ProgressUIViewModel
{
    [Inject] private UIEvent _uiEvent;

    private float _currentProgress;
    private float _maxProgress = 1f;

    public float CurrentProgress
    {
        get { return _currentProgress; }
        set
        {
            if(_currentProgress > _maxProgress)
            {
                return;
            }
            _currentProgress += value;
            OnPropertyChanged(nameof(CurrentProgress));
        }
    }

    public void OnResponseChangeCurrentStage(float stage)
    {
        CurrentProgress = stage;
    }

    public void RegisterChangeProgressUIOnEnable()
    {
        _uiEvent.RegisterChangeProgressUI(this);
    }

    public void UnRegisterChangeProgressUI(ProgressUIViewModel viewModel)
    {
        _uiEvent.UnRegisterChangeProgressUI(this);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
