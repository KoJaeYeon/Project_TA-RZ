using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Run
}

public class PlayerStateMachine : MonoBehaviour
{
    private Dictionary<State, PlayerBaseState> _stateDic = new Dictionary<State, PlayerBaseState>();
    private PlayerBaseState _currentState;

    private void Start()
    {
        _currentState = _stateDic[State.Idle];
        _currentState.StateEnter();
    }

    private void Update()
    {
       _currentState.StateUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void ChangeState(State changeState)
    {
        _currentState.StateExit();

        _currentState = _stateDic[changeState];

        _currentState.StateEnter();
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
