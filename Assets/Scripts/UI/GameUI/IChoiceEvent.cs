using System;
using UnityEngine;

public interface IChoiceEvent
{
    public void GetChoiceStageEvent(bool isAddEvent, Action<Player> callBack);
}
