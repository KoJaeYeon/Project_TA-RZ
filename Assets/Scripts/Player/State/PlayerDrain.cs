using UnityEngine;
using Zenject;

public class PlayerDrain : PlayerState
{    
    public PlayerDrain(Player player) : base(player)
    {
        _animator = player.GetComponentInChildren<Animator>();
        _controller = player.GetComponent<CharacterController>();
        _inputSystem = player.GetComponent<PlayerInputSystem>();
        _state = player.GetComponent<PlayerStateMachine>();
        _drainSystem = player.GetComponentInChildren<DrainSystem>();
        _sphereCollider = _drainSystem.GetComponent<SphereCollider>();
        _drainSystem.gameObject.SetActive(false);
    }

    #region RunComponent
    private Animator _animator;
    private Rigidbody _rigidBody;
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;
    private CharacterController _controller;
    private DrainSystem _drainSystem;
    private SphereCollider _sphereCollider;
    #endregion

    #region DrainValue
    private float _currentDrainRadius;
    private float _maxDrainRadius = 5;
    private float _drainSpeed = 1;
    #endregion

    #region AnimatorStringToHash
    private readonly int _drain = Animator.StringToHash("Drain");
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
        _drainSystem.gameObject.SetActive(true);
        _currentDrainRadius = 0;
    }

    void UpdateDrain()
    {
        DrainInputCheck();
        DrainRangeControl();
    }

    void EndDrain()
    {
        _animator.SetBool(_drain, false);
        _drainSystem.gameObject.SetActive(false);
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
            Debug.Log(_currentDrainRadius);
            _sphereCollider.radius = _currentDrainRadius;
        }
    }
}
