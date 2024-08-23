using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressUIViewModel : MonoBehaviour
{
    private int _currentStage;

    public int CurrentStage
    {
        get { return _currentStage; }
        set
        {
            if(_currentStage == value)
            {
                return;
            }

            _currentStage = value;

        }
    }


   
}
