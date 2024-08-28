using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;


public class MapManager : MonoBehaviour
{
    #region InJect
    [Inject]
    private UIEvent _uiEvent;
    #endregion

    #region Component
    private Stage _currentStage;
    private Player _player;
    #endregion

    #region Value
    private float _progressValue;
    public float ProgressValue { get; set; }
    private StageType _currentStageType;
    #endregion

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

        if(_progressValue <= 0.33f)
        {
            Debug.Log("초반부");
            LoadSceneBeginning();
        }
        else if(_progressValue <= 0.66f)
        {
            Debug.Log("중반부");
            LoadSceneMiddle();
        }
        else if(_progressValue <= 0.99f)
        {
            Debug.Log("후반부");
            LoadSceneFinal();
        }
        else
        {
            Debug.Log("보스");
            LoadSceneBoss();
        }
    }

    private void LoadSceneBeginning()
    {
        Debug.Log("로드씬");
        LoadingScene.LoadScene("Beginning");
    }

    private void LoadSceneMiddle()
    {
        Debug.Log("로드씬");

    }

    private void LoadSceneFinal()
    {
        Debug.Log("로드씬");

    }

    private void LoadSceneBoss()
    {
        Debug.Log("로드씬");
    }

    public StageType GetStageType()
    {
        return _currentStageType;
    }

    public void RequestChangeProgressValue(float value)
    {
        _progressValue += value;

        _uiEvent.RequestChangeProgressBar(value);
    }
}
