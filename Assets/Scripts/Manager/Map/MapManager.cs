using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    private Stage _currentStage;

    private float _progressValue;

    private StageType _currentStageType;

    public float ProgressValue 
    {
        get { return _progressValue; }
        set {  _progressValue = value; } 
    }

    private void Awake()
    {
        _progressValue = 0f;
    }

    public void SetStage(StageType newStage, Player player, float currentProgress)
    {
        _currentStageType = newStage;






    }

    
}
