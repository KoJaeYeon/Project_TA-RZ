using UnityEngine;
using UnityEngine.Windows;


public class PlayerRun : PlayerState
{
    public PlayerRun(Player player) : base(player) { }
   
    #region RunValue
    private float _runSpeed = 5f;
    private float _targetSpeed;
    private float _speedOffset = 0.1f;
    private float _currentSpeed;
    private float _speedChangevalue = 100f;
    private float _rotationChangevalue = 0.12f;
    private float _rotationVelocity;
    private float _targetRotation;
    private float _sphereRadius = 0.2f;
    
    private Vector3 _spherePosition;
    
    private bool _isGround;
    private bool _isAction = false;
    #endregion

    #region State
    public override void StateEnter()
    {
        InitializeRun();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        OnUpdateRun();
    }

    public override void StateExit()
    {
        Exit();
    }
    #endregion

    private void InitializeRun()
    {
        _isAction = false;
    }

    private void OnUpdateRun()
    {
        PlayerMove();
    }

    private void Exit()
    {
        _speedChangevalue = 5f;
    }


    private void PlayerMove()
    {
        CheckGround();

        if (!_isGround)
            return;

        if (!_isAction)
        {
            _targetSpeed = _runSpeed;

            if (_inputSystem.Input == Vector2.zero)
            {
                _targetSpeed = 0f;

                _speedChangevalue = 5f;

                if (_currentSpeed == _targetSpeed)
                {
                    _state.ChangeState(State.Idle);

                    return;
                }

            }
            else
            {
                if (_speedChangevalue != 100f)
                {
                    _speedChangevalue = Mathf.Lerp(_speedChangevalue, 100f, Time.deltaTime);
                }
            }

            float currentHorizontalSpeed = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z).magnitude;

            bool speedCorrection = currentHorizontalSpeed < _targetSpeed - _speedOffset
                || currentHorizontalSpeed > _targetSpeed + _speedOffset;

            if (speedCorrection)
            {
                _currentSpeed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed, _speedChangevalue * Time.deltaTime);

                _currentSpeed = Mathf.Round(_currentSpeed * 1000f) / 1000f;
            }
            else
            {
                _currentSpeed = _targetSpeed;
            }

            Vector3 inputDirection = new Vector3(_inputSystem.Input.x, 0f, _inputSystem.Input.y).normalized;

            if (_inputSystem.Input != Vector2.zero)
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

            _animator.SetFloat("Speed", _currentSpeed);
        }
        else
        {
            _animator.SetFloat("Speed", 0f);

            _rigidBody.velocity = Vector3.zero;
        }
            
        
    }

    private void CheckGround()
    {
        _spherePosition = new Vector3(_player.transform.position.x,
            _player.transform.position.y, _player.transform.position.z);

        _isGround = Physics.CheckSphere(_spherePosition, _sphereRadius, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore);
    }

    public override void InputCheck()
    {
        if (_inputSystem.IsDash && _player.StaminaCheck())
        {
            _state.ChangeState(State.Dash);
        }
        else if (_inputSystem.IsAttack)
        {
            _isAction = true;
            _state.ChangeState(State.FirstComboAttack);
        }
        else if (_inputSystem.IsDrain && _player.StaminaCheck())
        {
            _isAction = true;
            _state.ChangeState(State.Drain);
        }
        else if(_inputSystem.IsSkill && _player.SkillCheck())
        {
            _isAction = true;
            _state.ChangeState(State.Skill);
        }
    }
}
