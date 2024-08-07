using Unity.VisualScripting;
using UnityEngine;

public class PlayerRun : PlayerState
{
    private Animator _animator;
    private CharacterController _controller;
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;

    private float _walkSpeed = 5f;
    private float _runSpeed = 10f;
    private float _targetSpeed;
    private float _speedOffset = 0.1f;
    private float _currentSpeed;
    private float _changeValue = 10f;
    private float _rotationVelocity;
    private float _targetRotation;

    private readonly int _move = Animator.StringToHash("Walk");

    public PlayerRun(Player player) : base(player)
    {
        _animator = player.GetComponent<Animator>();
        _controller = player.GetComponent<CharacterController>();
        _inputSystem = player.GetComponent<PlayerInputSystem>();
        _state = player.GetComponent<PlayerStateMachine>();
    }

    public override void StateEnter()
    {
        _animator.SetBool(_move, true);
    }

    public override void StateUpdate()
    {
        PlayerMove();
    }

    public override void StateExit()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        
    }

    private void PlayerMove()
    {
        _targetSpeed = _inputSystem.IsRun ? _runSpeed : _walkSpeed; 

        if(_inputSystem.Input == Vector2.zero)
        {
            _targetSpeed = 0f;

            _animator.SetBool(_move, false);

            _state.ChangeState(State.Idle);
        }

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z).magnitude;

        bool speedCorrection = currentHorizontalSpeed < _targetSpeed - _speedOffset
            || currentHorizontalSpeed > _targetSpeed + _speedOffset;

        if(speedCorrection)
        {
            _currentSpeed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed, _changeValue * Time.deltaTime);

            _currentSpeed = Mathf.Round(_currentSpeed * 1000f) / 1000f;
        }
        else
        {
            _currentSpeed = _targetSpeed;
        }

        Vector3 inputDirection = new Vector3(_inputSystem.Input.x, 0f, _inputSystem.Input.y).normalized;

        if(_inputSystem.Input != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                _player.MainCamera.transform.eulerAngles.y;

            float smoothAngle = Mathf.SmoothDampAngle(_player.transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, 0.12f);

            _player.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

        _animator.SetFloat("Speed", _currentSpeed);

        _controller.Move(targetDirection.normalized * (_currentSpeed * Time.deltaTime)); 

    }



}
