using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject]
    private PlayerManager _playerManager;
    [Inject] DataManager dataManager;
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;
    private Camera _camera;
    private Action<float, float, float, int, float> _statChangeCallback;

    PC_Common_Stat _playerStat = new PC_Common_Stat();

    public Camera MainCamera { get { return _camera; } }

    #region PlayerValue
    float _currentHP;
    float _currentSkill;
    float _currentStamina;
    int _currentAmmo;

    public float CurrentHP
    {
        get { return _currentHP; }
        set
        {
            if (_currentHP == value)
                return;

            _currentHP = value;
            OnPropertyChanged(nameof(CurrentHP));
        }
    }
    public float CurrentSkill
    {
        get { return CurrentSkill; }
        set
        {
            if (_currentSkill == value)
                return;

            _currentSkill = value;
            OnPropertyChanged(nameof(CurrentSkill));
        }
    }
    public float CurrentStamina
    {
        get { return _currentStamina; }
        set
        {
            if (_currentStamina == value)
                return;

            _currentStamina = value;
            OnPropertyChanged(nameof(CurrentStamina));
        }
    }
    public int CurrentAmmo
    {
        get { return _currentAmmo; }
        set
        {
            if (_currentAmmo == value)
                return;

            _currentAmmo = value;
            OnPropertyChanged(nameof(CurrentAmmo));
        }
    }

    public bool IsNext {  get; set; }
    #endregion

    #region PropChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private void Awake()
    {
        InitializeComponent();
        InitializePlayer();
        InitializeState();
    }

    private void InitializePlayer()
    {
        _playerManager.SetPlayerObject(gameObject);
        StartCoroutine(LoadStat());
    }

    private void InitializeComponent()
    {
        _state = gameObject.AddComponent<PlayerStateMachine>();
        _inputSystem = gameObject.AddComponent<PlayerInputSystem>();
        _camera = Camera.main;
    }

    private void InitializeState()
    {
        _state.AddState(State.Idle, new PlayerIdle(this));
        _state.AddState(State.Run, new PlayerRun(this));
        _state.AddState(State.Dash, new PlayerDash(this));  
        _state.AddState(State.Drain, new PlayerDrain(this));
        _state.AddState(State.FirstComboAttack, new PlayerFirstComboAttack(this));
        _state.AddState(State.SecondComboAttack, new PlayerSecondComboAttack(this));
        _state.AddState(State.ThirdComboAttack, new PlayerThirdComboAttack(this));
        _state.AddState(State.FourthComboAttack, new PlayerFourthComboAttack(this));
        _state.OnDamagedStateChange();
    }

    IEnumerator LoadStat()
    {
        while (true)
        {
            _playerStat = dataManager.GetStat("P101") as PC_Common_Stat;
            if( _playerStat == null )
            {
                Debug.Log("Player의 스탯을 받아오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                Debug.Log("Player의 스탯을 성공적으로 받아왔습니다.");
                CurrentHP = _playerStat.HP;
                CurrentStamina = 0;
                CurrentSkill = 0;
                CurrentAmmo = 0;
                yield break;
            }

        }
    }

    //checkGround 
    private void OnDrawGizmos()
    {
        Vector3 GizmoPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.DrawWireSphere(GizmoPosition, 0.2f);
    }
}
