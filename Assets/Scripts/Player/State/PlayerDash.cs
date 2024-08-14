using System.Collections;
using UnityEngine;

public class PlayerDash : PlayerState
{
    public PlayerDash(Player player) : base(player) { }
    private WaitForSeconds _coolTime = new WaitForSeconds(0.5f);

    private float _dashPower = 15f;
    private float _dashTime;
    private float _maxTime = 0.3f;

    private bool _canDash = true;

    private Vector3 _dashDirection;

    public override void StateEnter()
    {
        _player.CurrentStamina -= _player._playerStat.Dash_Stamina;

        if (_canDash)
        {
            InitializeDash();
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

    public override void OnTriggerEnter(Collider other)
    {
        //피격
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

                _state.ChangeState(State.Idle);
            }
        }
    }

    private void Exit()
    {
        _inputSystem.SetDash(false);
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

    public override void InputCheck()
    {
        if (_inputSystem.IsSkill)
        {
            _state.ChangeState(State.Skill);
        }
    }
}
