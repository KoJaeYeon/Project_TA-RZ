using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;


public class MapManager : MonoBehaviour
{
    [Inject]
    private UIEvent _uiEvent;
    private Stage _currentStage;
    private Player _player;

    private float _progressValue;
    public float ProgressValue { get; set; }

    private StageType _currentStageType;

    private void Awake()
    {
        _progressValue = 0f;
    }

    public void SetStage(Stage currentStage)
    {
        _currentStage = currentStage;
    }

    public void ChoiceMap(StageType newStage, Player player)
    {
        _currentStageType = newStage;

        _player = player;

        if(_progressValue < 0.33f)
        {
            LoadSceneBeginning();
        }
        else if(_progressValue < 0.66f)
        {
            LoadSceneMiddle();
        }
        else if(_progressValue <= 0.99f)
        {
            LoadSceneFinal();
        }
        else
        {
            LoadSceneBoss();
        }
    }

    public void RequestChangeProgressValue(float value)
    {
        _progressValue += value;

        _uiEvent.RequestChangeProgressBar(value);
    }

    private void LoadSceneBeginning()
    {

    }

    private void LoadSceneMiddle()
    {

    }

    private void LoadSceneFinal()
    {

    }

    private void LoadSceneBoss()
    {

    }

    public StageType GetStageType()
    {
        return _currentStageType;
    }
}
