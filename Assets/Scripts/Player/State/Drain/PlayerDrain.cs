using UnityEngine;
using Zenject;

public class PlayerDrain : PlayerState
{    
    public PlayerDrain(Player player) : base(player)
    {
        _animator = player.GetComponentInChildren<Animator>();
        _inputSystem = player.GetComponent<PlayerInputSystem>();
        _state = player.GetComponent<PlayerStateMachine>();
        _drainSystem = player.GetComponentInChildren<DrainSystem>();
        _sphereCollider = _drainSystem.GetComponent<SphereCollider>();
    }

    #region DrainComponent
    private DrainSystem _drainSystem;
    private SphereCollider _sphereCollider;
    #endregion

    #region DrainValue
    private float _currentDrainRadius;
    private float _maxDrainRadius = 5;
    private float _drainSpeed = 2;
    #endregion

    public override void StateEnter()
    {
        StartDrain();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        UpdateDrain();
    }

    public override void StateExit()
    {
        EndDrain();
    }

    void StartDrain()
    {
        _animator.SetBool(_drain, true);
        _currentDrainRadius = 1;
        _drainSystem.OnSetActiveDraintEffect(true);
        _player.IsActiveStaminaRecovery = false;
    }

    void UpdateDrain()
    {        
        DrainRangeControl();
        DrainStaminaUse();
    }

    void EndDrain()
    {
        _animator.SetBool(_drain, false);
        _currentDrainRadius = 1;
        _drainSystem.OnSetActiveDraintEffect(false);        
        _inputSystem.SetDrain(false);
        if (_player.CurrentStamina > 0) _player.IsActiveStaminaRecovery = true;
    }

    /// <summary>
    /// 드레인 시 스태미나 사용하는 함수
    /// </summary>
    void DrainStaminaUse()
    {
        _player.CurrentStamina -= _player._playerStat.Drain_Stamina * Time.deltaTime;
        if(_player.CurrentStamina <= 0)
        {
            _state.ChangeState(State.Idle);
        }
    }

    /// <summary>
    /// 드레인 키가 활성화 되어 있는지 확인하는 함수
    /// </summary>
    public override void InputCheck()
    {
        if (_inputSystem.IsDrain == false)
        {
            _state.ChangeState(State.Idle);
        }
        else if(_inputSystem.IsDash == true && _player.StaminaCheck())
        {
            _state.ChangeState(State.Dash);
        }
        //else if(_inputSystem.IsSkill == true)
        //{
        //    _state.ChangeState(State.Skill);
        //}
    }

    /// <summary>
    /// 드레인 범위 증가
    /// </summary>
    void DrainRangeControl()
    {
        if (_currentDrainRadius < _maxDrainRadius)
        {
            _currentDrainRadius += _drainSpeed * Time.deltaTime;
            _sphereCollider.radius = _currentDrainRadius;
            _drainSystem.OnSetDrainArea(_currentDrainRadius);
        }
    }
}
