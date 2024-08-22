using System.Collections;
using UnityEngine;
using Zenject;
using QFX.SFX;

public class PlayerDash : PlayerState
{
    public PlayerDash(Player player) : base(player) { }
    private WaitForSeconds _coolTime = new WaitForSeconds(0.5f);

    private float _dashPower = 15f;
    private float _dashTime;
    private float _maxTime = 0.3f;
    private float _activateCloneplay = 0.1f;
    private float _time;

    private bool _canDash = true;

    private Vector3 _dashDirection;

    public override void StateEnter()
    {       
        if (_canDash)
        {
            InitializeDash();
            _player.CurrentStamina -= _player._playerStat.Dash_Stamina;
        }
        else
            _state.ChangeState(State.Idle);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        OnUpdateDash();
    }

    public override void StateExit()
    {
        Exit();
    }

    private void InitializeDash()
    {
        _canDash = false;

        _animator.SetTrigger(_dashAnimation);

        _rigidBody.velocity = Vector3.zero;

        Dash();

        StartCoolTime();
    }

    private void OnUpdateDash()
    {
        if (_rigidBody != null)
        {
            _dashTime += Time.deltaTime;

            _time += Time.deltaTime;
            
            if (_time >= _activateCloneplay)
            {
                _player.Cloner.MakeClone();

                _time = 0f;
            }

            if (_dashTime >= _maxTime)
            {
                _rigidBody.velocity = Vector3.zero;

                _rigidBody.angularVelocity = Vector3.zero;

                _state.ChangeState(State.Idle);
            }
        }
    }

    private void Exit()
    {
        _player.Cloner.Stop();
        _inputSystem.SetDash(false);
        _dashTime = 0f;
    }

    private void Dash()
    {
        if (_inputSystem.Input == Vector2.zero)
        {
            _player.AllgnToCamera();
        }
        else
        {
            //Vector3 inputDirection = new Vector3(_inputSystem.Input.x, 0f, _inputSystem.Input.y).normalized;
            //float _targetRotation;

            //_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) +
            //    _player.MainCamera.transform.eulerAngles.y;

            //_player.transform.rotation = Quaternion.Euler(0f, _targetRotation, 0f);
        }

        _player.Cloner.Run();

        _dashDirection = _player.transform.forward;

        Vector3 dash = _dashDirection * _dashPower;

        _rigidBody.AddForce(dash, ForceMode.Impulse);
    }

    public void StartCoolTime()
    {
        _player.StartCoroutine(DashCoolTime());
    }

    private IEnumerator DashCoolTime()
    {
        yield return _coolTime;

        _canDash = true;
    }

    public override void InputCheck()
    {
        if (_inputSystem.IsSkill && _player.SkillCheck())
        {
            _rigidBody.velocity = Vector3.zero;

            _rigidBody.angularVelocity = Vector3.zero;

            _state.ChangeState(State.Skill);
        }
    }
}
