using UnityEngine;

public class PlayerDash : PlayerState
{
    public PlayerDash(Player player) : base(player) { }
    
    private float _dashPower = 10f;
    private float _dashTime;
    private float _maxTime = 1.5f;

    private Vector3 _dashDirection;

    private State _previousState;

    public override void StateEnter()
    {
        InitializeDash();
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
        _previousState = _state.PreviousState;

        _rigidBody.velocity = Vector3.zero;

        _rigidBody.useGravity = false;

        Dash();
    }

    private void OnUpdateDash()
    {
        if (_rigidBody != null)
        {
            _dashTime += Time.deltaTime;

            if(_dashTime >= _maxTime)
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
        _rigidBody.useGravity = true;
    }

    private void Dash()
    {
        _dashDirection = _player.transform.forward;

        Vector3 dash = _dashDirection * _dashPower;

        _rigidBody.AddForce(dash, ForceMode.Impulse);
    }

    //private bool IsImpact()
    //{
    //    Collider[] colliders = Physics.OverlapBox()
    //}
}
