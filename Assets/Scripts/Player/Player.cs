using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject]
    private PlayerManager _playerManager;
    private PlayerInputSystem _inputSystem;
    private PlayerStateMachine _state;
    private Camera _camera;

    public Camera MainCamera { get { return _camera; } }

    #region PlayerValue
    public int currentAmmo { get; set; }
    #endregion


    private void Awake()
    {
        InitializeComponent();
        InitializePlayer();
        InitializeState();
    }

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

    }

    //checkGround 
    private void OnDrawGizmos()
    {
        Vector3 GizmoPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.DrawWireSphere(GizmoPosition, 0.2f);

        Vector3 BoxGizomPosition = transform.position + new Vector3(0f,0.7f,0.2f);
        Gizmos.DrawWireCube(BoxGizomPosition, new Vector3(1f, 1f, 1f));

    }
}
