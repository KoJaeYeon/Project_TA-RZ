using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IHit
{
    [Inject]
    private PlayerManager _playerManager;
    [Inject] DataManager dataManager;
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;
    private Rigidbody _rigidBody;
    private Camera _camera;
    private Action<float, float, float, int, float> _statChangeCallback;

    public PC_Common_Stat _playerStat { get; private set; } = new PC_Common_Stat();

    public Camera MainCamera { get { return _camera; } }

    #region PlayerValue
 
    float _currentHP;
    float _currentSkill;
    float _currentStamina;
    int _currentAmmo;
    public bool IsActiveStaminaRecovery { get; set; } = true;


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
        get { return _currentSkill; }
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
            if (value <= 0)
            {
                value = 0;
            }
            else if (value > 100)
            {
                value = 100;
            }

            if (_currentStamina == value)
                return;

            _currentStamina = value;

            if (_currentStamina == 0)
            {
                StartCoroutine(StaminaDelay());
            }
            
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
    public float HP
    {
        get { return _playerStat.HP; }
        set
        {
            if (_playerStat.HP == value)
                return;

            _playerStat.HP = value;
            OnPropertyChanged(nameof(HP));
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

    private void Update()
    {
        StaminaRecovery();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ApplyKnockback(-transform.forward);
        }
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
        _rigidBody = GetComponent<Rigidbody>();
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
        _state.AddState(State.Skill, new PlayerSkill(this));
        _state.AddState(State.Hit, new PlayerHit(this));    
        _state.AddState(State.KnockBack, new PlayerKnockBack(this));
        _state.AddState(State.Death, new PlayerDeath(this));    
    }

    IEnumerator StaminaDelay()
    {
        IsActiveStaminaRecovery = false;
        yield return new WaitForSeconds(3f);
        IsActiveStaminaRecovery = true;
    }

    private void StaminaRecovery()
    {
        if(IsActiveStaminaRecovery == true)
        {
            CurrentStamina += _playerStat.Stamina_Gain * Time.deltaTime;
        }
    }

    public bool StaminaCheck()
    {
        return CurrentStamina > 0;
    }

    IEnumerator LoadStat()
    {
        while (true)
        {
            var stat = dataManager.GetStat("P101") as PC_Common_Stat;
            if(stat == null )
            {
                Debug.Log("Player의 스탯을 받아오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                _playerStat = stat;
                Debug.Log("Player의 스탯을 성공적으로 받아왔습니다.");
                CurrentHP = _playerStat.HP;
                HP = _playerStat.HP;
                CurrentStamina = 100;
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

    #region Hit

    public void Hit(float damage)
    {
        Debug.Log(_currentHP);

        if(_currentHP <= 0)
        {
            _state.ChangeState(State.Death);

            return;
        }

        CurrentHP -= damage;

        _state.OnDamagedStateChange();
    }

    public void ApplyKnockback(Vector3 otherPosition)
    {
        PlayerKnockBack._knockBackPosition = otherPosition;

        _state.OnKnockBackStateChange();
    }

    #endregion
}
