using UnityEngine;

public class PlayerDash : PlayerState
{
    public PlayerDash(Player player) : base(player) { }
    
    private float _dashPower = 15f;
    private float _dashTime;
    private float _maxTime = 0.3f;

    private Vector3 _dashDirection;

    private State _previousState;

    private int _dashAnimation = Animator.StringToHash("Dash");

    private bool _canDash = true;

    //광클 못하게 해야함 
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
        if (_canDash)
        {
            _canDash = false;

            _animator.SetBool(_dashAnimation, true);

            _previousState = _state.PreviousState;

            _rigidBody.velocity = Vector3.zero;

            Dash();
        }

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

            //var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            //bool stopDash = animatorStateInfo.normalizedTime >= 1f;

            //if(stopDash)
            //{
            //    _rigidBody.velocity = Vector3.zero;

            //    _rigidBody.angularVelocity = Vector3.zero;

            //    _state.ChangeState(_previousState);
            //}
        }
    }

    private void Exit()
    {
        _dashTime = 0f;
        _rigidBody.useGravity = true;
        _canDash = true;
        _animator.SetBool(_dashAnimation, false);
    }

    private void Dash()
    {
        _dashDirection = _player.transform.forward;

        Vector3 dash = _dashDirection * _dashPower;

        _rigidBody.AddForce(dash, ForceMode.Impulse);
    }

    
}
