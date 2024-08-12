using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDash : PlayerState
{
    public PlayerDash(Player player) : base(player) { }
    private WaitForSeconds _coolTime = new WaitForSeconds(0.7f);

    private float _dashPower = 15f;
    private float _dashTime;
    private float _maxTime = 0.3f;

    private bool _canDash = true;

    private Vector3 _dashDirection;

    private State _previousState;

    public override void StateEnter()
    {

        _previousState = _state.CurrentState;

        if (_canDash)
        {
            InitializeDash();
        }
        else
            _state.ChangeState(_previousState);
    }

    public override void StateUpdate()
    {
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

            if (_dashTime >= _maxTime)
            {
                _rigidBody.velocity = Vector3.zero;

                _rigidBody.angularVelocity = Vector3.zero;

                _state.ChangeState(_previousState);
            }
        }
    }

    private void Exit()
    {
        _dashTime = 0f;
    }

    private void Dash()
    {
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
}
