using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
    KnockBack
}

public class PlayerStateMachine : MonoBehaviour
{
    private Dictionary<State, PlayerBaseState> _stateDic = new Dictionary<State, PlayerBaseState>();
    private PlayerBaseState _state;
    private Action _idleChangeEvent;

    public Action IdleEvent { get; private set; }   

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

    //Idle
    public void OnChangeStateIdle()
    {

    }

    //Run
    public void OnChangeStateRun()
    {

    }

    //Dash
    public void OnChangeStateDash()
    {

    }

    //Drain
    public void OnChangeStateDrain()
    {

    }

    //Attack
    public void OnChangeStateAttack()
    {

    }

    //Skill
    public void OnChangeStateSkill()
    {

    }
    
    //Hit
    public void OnDamagedChangeState()
    {
        bool change = _state is PlayerSkill;

        if (change)
        {
            return;
        }
        else
            ChangeState(State.Hit);
    }

    //KnockBack
    public void OnChangeStateKnockBack()
    {

    }

    public void ChangeState(State changeState)
    {
        if(_stateDic.TryGetValue(changeState, out PlayerBaseState value))
        {
            _state.StateExit();

            _state = value;

            _state.StateEnter();
        }
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
