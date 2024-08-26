using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    private Stage _currentStage;
    private Player _player;

    private float _progressValue;

    private StageType _currentStageType;

    public float ProgressValue { get; set; }
    
    private void Awake()
    {
        _progressValue = 0f;
    }

    public void SetStage(Stage currentStage)
    {
        _currentStage = currentStage;
    }

    public void ChoiceMap(StageType newStage, Player player, float progressValue)
    {
        _currentStageType = newStage;

        _player = player;

        _progressValue = progressValue;
    }

    public StageType GetStageType()
    {
        return _currentStageType;
    }
}
