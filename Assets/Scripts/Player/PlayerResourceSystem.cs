using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class PlayerResourceSystem : MonoBehaviour
{
    Player _player;
    [Inject] DataManager _dataManager;

    [SerializeField] GameObject[] ArmUnitEffects; 

    private int _currentLevel;

    private int[] _needResource = new int[4];

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        //_player = GetComponent<Player>();
        if (_player != null)
        {
            _player.PropertyChanged += OnPropertyChanged;
            StartCoroutine(LoadData("P501"));
            StartCoroutine(LoadInitialData());
        }
        else
        {
            Debug.LogError("PlayerView Is Null!");
        }
    }

    /// <summary>
    /// 플레이어의 레벨 데이터를 변경해주는 함수
    /// </summary>
    /// <param name="idStr"></param>
    /// <returns></returns>
    IEnumerator LoadData(string idStr)
    {
        while (true)
        {
            var level = _dataManager.GetData(idStr) as PC_Level;
            if (level == null)
            {
                Debug.Log("Player의 암유닛 레벨을 받아오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                _player.Set_PC_Level(level);
                Debug.Log("Player의 암유닛 레벨을 성공적으로 받아왔습니다.");
                yield break;
            }

        }
    }

    IEnumerator LoadInitialData()
    {
        while (true)
        {
            var level = _dataManager.GetData("P501") as PC_Level;
            if (level == null)
            {
                Debug.Log("Player의 암유닛 필요 자원을 받아오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                for(int i = 0; i < 4; i++)
                {
                    level = _dataManager.GetData($"P50{i+1}") as PC_Level;
                    _needResource[i] = level.Level_Min_Require;
                }
                Debug.Log("Player의 암유닛 필요 자원을 성공적으로 받아왔습니다.");
                yield break;
            }

        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_player.CurrentAmmo):
                ChangeLevel(_player.CurrentAmmo);
                break;
        }
    }

    void ChangeLevel(int currentAmmo)
    {
        if(false)
        {
            //4단계 조건 성공 시
            if (_currentLevel == 3) return;

            StartCoroutine(LoadData("P504"));
            _currentLevel = 3;

            EffectAcitve(6, 7);
        }
        else if(currentAmmo >= _needResource[3])
        {
            if (_currentLevel == 3) return;

            StartCoroutine( LoadData("P504"));
            _currentLevel = 3;

            EffectAcitve(4, 5);

        }
        else if (currentAmmo >= _needResource[2])
        {
            if (_currentLevel == 2) return;

            StartCoroutine(LoadData("P503"));
            _currentLevel = 2;

            EffectAcitve(2, 3);
        }
        else if (currentAmmo >= _needResource[1])
        {
            if (_currentLevel == 1) return;

            StartCoroutine(LoadData("P502"));
            _currentLevel = 1;

            EffectAcitve(0, 1);
        }
        else if (currentAmmo >= _needResource[0])
        {
            if (_currentLevel == 0) return;

            StartCoroutine(LoadData("P501"));
            _currentLevel = 0;

            EffectAcitve(-1, -1);
        }
    }

    void EffectAcitve(int left, int right)
    {
        int index = 0;
        foreach (var item in ArmUnitEffects)
        {
            if (index == left || index == right)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
            index++;
        }
    }
}
