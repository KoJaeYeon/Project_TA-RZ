using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject]
    private PlayerManager _playerManager;
    private PlayerStateMachine _state;
    private PlayerInputSystem _inputSystem;
    private Camera _camera;

    public Camera MainCamera { get { return _camera; } }    
    
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
    }
}
