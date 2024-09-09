using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Zenject;

public class PlayerInfoUI : MonoBehaviour
{
    [Inject]
    private UIEvent _uiEvent;

    [SerializeField] private InputActionReference _tabAction;
    [SerializeField] private GameObject[] _childObject;

    private void OnEnable()
    {
        _tabAction.action.Enable();
        _tabAction.action.performed += OnInfoUI;
    }

    private void OnDisable()
    {
        _tabAction.action.performed -= OnInfoUI;
        _tabAction.action.Disable();
    }

    public void OnEnableInfoUI()
    {
        _uiEvent.SetActivePlayerUI(false);
        OnChildObject();
    }
    
    private void OnInfoUI(InputAction.CallbackContext callbackContext)
    {
        if (_childObject[0].activeSelf)
        {
            OnChildObject();
            _uiEvent.SetActivePlayerUI(true);
        }
    }

    public void OnChildObject()
    {
        for (int i = 0; i < _childObject.Length; i++)
        {
            _childObject[i].SetActive(!_childObject[i].activeSelf);
        }
    }
}
