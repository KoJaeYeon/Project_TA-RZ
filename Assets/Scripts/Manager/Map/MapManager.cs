using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public enum StartPosition
{
    Beginning,
    Middle,
    Final
}

public class MapManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private List<GameObject> _mapList;

    [Header("ResetPosition")]
    [SerializeField] private Transform[] _resetPosition;

    #region InJect
    [Inject]
    private UIEvent _uiEvent;
    #endregion

    #region Component
    private Stage _currentStage;
    private Player _player;
    #endregion

    #region Value
    public float ProgressValue { get; set; }
    private StageType _currentStageType;
    #endregion

    private Dictionary<StartPosition, Transform> _resetPositionDictionary;

    private void Awake()
    {
        InitializeMapManager();
    }

    private void InitializeMapManager()
    {
        ResetPositionToDictionary();
    }

    #region Initialize
    private void ResetPositionToDictionary()
    {
        if(_resetPosition.Length != System.Enum.GetValues(typeof(StartPosition)).Length)
        {
            Debug.Log("enum 길이와 Transform 배열의 크기가 일치하지 않습니다.");
            return;
        }

        _resetPositionDictionary = new Dictionary<StartPosition, Transform>();

        for(int i = 0; i < _resetPosition.Length; i++)
        {
            _resetPositionDictionary.Add((StartPosition)i, _resetPosition[i]);
        }
    }

    #endregion

    public void SetStage(Stage currentStage)
    {
        _currentStage = currentStage;
    }

    public void ChoiceMap(StageType newStage, Player player)
    {
        _currentStageType = newStage;

        _player = player;

        if(ProgressValue <= 0.33f)
        {
            Debug.Log("초반부");
            LoadSceneBeginning();
        }
        else if(ProgressValue <= 0.66f)
        {
            Debug.Log("중반부");
            LoadSceneMiddle();
        }
        else if(ProgressValue <= 0.99f)
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
        LoadingScene.LoadScene("Beginning");
    }

    private void LoadSceneMiddle()
    {
        LoadingScene.LoadScene("Middle");
    }

    private void LoadSceneFinal()
    {
        LoadingScene.LoadScene("Final");
    }

    private void LoadSceneBoss()
    {
        LoadingScene.LoadScene("Boss");
    }

    public StageType GetStageType()
    {
        return _currentStageType;
    }

 
}
