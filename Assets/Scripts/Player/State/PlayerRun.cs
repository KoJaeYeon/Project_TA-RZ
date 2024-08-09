using UnityEngine;


public class PlayerRun : PlayerState
{
    public PlayerRun(Player player) : base(player) { }
   
    #region RunValue
    private float _runSpeed = 5f;
    private float _targetSpeed;
    private float _speedOffset = 0.1f;
    private float _currentSpeed;
    private float _speedChangevalue = 10f;
    private float _rotationChangevalue = 0.12f;
    private float _rotationVelocity;
    private float _targetRotation;
    private float _sphereRadius = 0.2f;
    private float _noInputTime;
    private float _maxNoInputTime = 0.08f;

    private Vector3 _spherePosition;
    
    private bool _isGround;
    #endregion

    public override void StateEnter()
    {
        InitializeRun();
    }

    
    public override void StateUpdate()
    {
        OnUpdateRun();
    }

    
    public override void StateExit()
    {
        Exit();
    }

    private void InitializeRun()
    {
        if(_state.CurrentState == State.Dash)
        {
            _animator.SetInteger(_dashChange, (int)State.Run);
        }

        _state.CurrentState = State.Run;

        _animator.SetBool(_moveAnimation, true);
    }

    private void OnUpdateRun()
    {
        if (_inputSystem.IsDash)
        {
            _state.ChangeState(State.Dash);
        }

        PlayerMove();
    }

    private void Exit()
    {
        
    }

    private void PlayerMove()
    {
        CheckGround();

        if (!_isGround)
            return;

        _targetSpeed = _runSpeed;

        if(_inputSystem.Input == Vector2.zero)
        {
            _noInputTime += Time.deltaTime;

            if(_noInputTime >= _maxNoInputTime)
            {
                _targetSpeed = 0f;

                _rigidBody.velocity = new Vector3(0f, _rigidBody.velocity.y, 0f);

                _animator.SetBool(_moveAnimation, false);

                _state.ChangeState(State.Idle);

                return;
            }
        }
        else
        {
            _noInputTime = 0f;
        }

        float currentHorizontalSpeed = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z).magnitude;

        bool speedCorrection = currentHorizontalSpeed < _targetSpeed - _speedOffset
            || currentHorizontalSpeed > _targetSpeed + _speedOffset;

        if(speedCorrection)
        {
            _currentSpeed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed, _speedChangevalue * Time.deltaTime);

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

            float smoothAngle = Mathf.SmoothDampAngle(_player.transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationChangevalue);

            _player.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;

        Vector3 movement = targetDirection.normalized * _currentSpeed;

        movement.y = _rigidBody.velocity.y;

        _rigidBody.velocity = movement;
    }

    private void CheckGround()
    {
        _spherePosition = new Vector3(_player.transform.position.x,
            _player.transform.position.y, _player.transform.position.z);

        _isGround = Physics.CheckSphere(_spherePosition, _sphereRadius, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore);
    }
}
