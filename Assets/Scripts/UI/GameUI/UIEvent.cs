using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent
{
    private IChoiceEvent _choiceStageEvent;
    private Action<float> _progressCallBack;
    

    public void NullTest()
    {
        Debug.Log("널아님");
    }

    #region ChoiceEvent
    public void RegisterChoiceStageEvent(IChoiceEvent choiceStageEvent)
    {
        if (_choiceStageEvent != null)
        {
            Debug.Log("현재 IChoiceEvent가 등록되어 있습니다.");
            return;
        }

        _choiceStageEvent = choiceStageEvent;
    }

    public void UnRegisterChoiceStageEvent()
    {
        if(_choiceStageEvent == null)
        {
            return;
        }

        _choiceStageEvent = null;
    }

    public void AddEventChoiceStageEvent(bool isAddEvent, Action<Player> callBack)
    {
        if(_choiceStageEvent == null)
        {
            Debug.Log("IChoiceEvent가 등록되지 않았습니다.");
            return;
        }

        if(callBack == null)
        {
            Debug.Log("callBack 함수가 null입니다.");
        }

        _choiceStageEvent.GetChoiceStageEvent(isAddEvent, callBack);
    }
    #endregion

    #region ProgressUIEvent
    //이 메서드 호출
    public void RequestChangeProgressBar(float currentValue)
    {
        _progressCallBack?.Invoke(currentValue);
    }

    public void RegisterChangeProgressUI(ProgressUIViewModel viewModel)
    {
        _progressCallBack += viewModel.OnResponseChangeCurrentStage;
    }

    public void UnRegisterChangeProgressUI(ProgressUIViewModel viewModel)
    {
        _progressCallBack -= viewModel.OnResponseChangeCurrentStage;
    }
    #endregion

}

