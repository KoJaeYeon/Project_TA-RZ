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
        //_drainSystem.gameObject.SetActive(true);
    }

    void UpdateDrain()
    {
        DrainInputCheck();
        DrainRangeControl();
    }

    void EndDrain()
    {
        _animator.SetBool(_drain, false);
        _currentDrainRadius = 1;
        //_drainSystem.gameObject.SetActive(false);
    }

    /// <summary>
    /// 드레인 키가 활성화 되어 있는지 확인하는 함수
    /// </summary>
    void DrainInputCheck()
    {
        if (_inputSystem.IsDrain == false)
        {
            _state.ChangeState(State.Idle);
        }
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
        }
    }
}
