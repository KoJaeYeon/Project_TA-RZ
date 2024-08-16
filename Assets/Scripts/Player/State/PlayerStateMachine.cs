using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
    Skill,
    Hit,
    KnockBack,
    Death
}

public class PlayerStateMachine : MonoBehaviour
{
    private Dictionary<State, PlayerBaseState> _stateDic = new Dictionary<State, PlayerBaseState>();
    private PlayerBaseState _state;

    public PlayerBaseState CurrentState { get { return _state; } }

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
        _state.OnTriggerEnter(other);
    }

    //Hit
    public void OnDamagedStateChange()
    {
        if(false)
        {
            return;
        }
        else if(_state is PlayerKnockBack)
        {
            return;
        }
        else
        {
            ChangeState(State.Hit);
        }
    }

    //KnockBack
    public void OnKnockBackStateChange()
    {
        if (false)
        {
            return;
        }
        else
        {
            ChangeState(State.KnockBack);
        }
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

    public virtual void StateUpdate()
    {
        InputCheck();
    }
    
    public virtual void StateExit() { }

    public virtual void OnTriggerEnter(Collider other) { }

    public abstract void InputCheck();
}
