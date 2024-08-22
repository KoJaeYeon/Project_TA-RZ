using System;

public interface IChoiceEvent
{
    public void GetChoiceStageEvent(bool isAddEvent, Func<StageType> callBack);
}
