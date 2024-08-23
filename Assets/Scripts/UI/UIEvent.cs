using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent
{
    private IChoiceEvent _choiceStageEvent;

    public void NullTest()
    {
        Debug.Log("널아님");
    }

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

    public void AddEventChoiceStageEvent(bool isAddEvent, Action<GameObject> callBack)
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


}
