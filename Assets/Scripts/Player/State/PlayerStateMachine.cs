using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
{
    Idle,
    Run,
    Drain,
    FirstComboAttack,
    SecondComboAttack,
    ThirdComboAttack,
    FourthComboAttack,
    Dash,
    Skill
}

public class PlayerStateMachine : MonoBehaviour
{
    private Dictionary<State, PlayerBaseState> _stateDic = new Dictionary<State, PlayerBaseState>();
    private PlayerBaseState _state;
    private State _currentState;

    public State CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    private void Start()
    {
        _state = _stateDic[State.Idle];
        _state.StateEnter();
    }

    private void Update()
    {
        _state.StateUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void ChangeState(State changeState)
    {
        _state.StateExit();

        _state = _stateDic[changeState];

        _state.StateEnter();
    }

    public void AddState(State newState, PlayerBaseState state)
    {
        _stateDic.Add(newState, state);
    }
}

public abstract class PlayerBaseState
{
    public virtual void StateEnter() { }
    public virtual void StateUpdate() { }
    public virtual void StateExit() { }
    public virtual void OnTriggerEnter(Collider other) { }
}
