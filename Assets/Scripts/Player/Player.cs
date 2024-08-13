using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IHit
{
    [Inject]
    private PlayerManager _playerManager;
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;
    private Camera _camera;

    public Camera MainCamera { get { return _camera; } }

    #region PlayerValue
    public int currentAmmo { get; set; }
    public bool IsNext {  get; set; }   
    #endregion


    private void Awake()
    {
        InitializeComponent();
        InitializePlayer();
        InitializeState();
    }

    #region InitializePlayer

    private void InitializePlayer()
    {
        _playerManager.SetPlayerObject(gameObject);
    }

    private void InitializeComponent()
    {
        _state = gameObject.AddComponent<PlayerStateMachine>();
        _inputSystem = gameObject.AddComponent<PlayerInputSystem>();
        _camera = Camera.main;
    }

    private void InitializeState()
    {
        _state.AddState(State.Idle, new PlayerIdle(this));
        _state.AddState(State.Run, new PlayerRun(this));
        _state.AddState(State.Dash, new PlayerDash(this));  
        _state.AddState(State.Drain, new PlayerDrain(this));
        _state.AddState(State.FirstComboAttack, new PlayerFirstComboAttack(this));
        _state.AddState(State.SecondComboAttack, new PlayerSecondComboAttack(this));
        _state.AddState(State.ThirdComboAttack, new PlayerThirdComboAttack(this));
        _state.AddState(State.FourthComboAttack, new PlayerFourthComboAttack(this));
        _state.AddState(State.Skill, new PlayerSkill(this));
        _state.AddState(State.Hit, new PlayerHit(this));
        _state.AddState(State.KnockBack, new PlayerKnockBack(this));
    }
    #endregion

    //checkGround 
    private void OnDrawGizmos()
    {
        Vector3 GizmoPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.DrawWireSphere(GizmoPosition, 0.2f);
    }

    public void Hit(float damage)
    {
        
    }

    public void ApplyKnockback()
    {
        
    }
}
