using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [Header("InputSystem")]
    [SerializeField] private Vector2 _input;

    [Header("Run")]
    [SerializeField] private bool isRun;
    [SerializeField] private bool isDrain;

    public Vector2 Input { get { return _input; } }
    public bool IsRun { get { return isRun; } }
    public bool IsDrain { get { return isDrain; } }

    private void OnMove(InputValue input)
    {
        SetMove(input.Get<Vector2>());  
    }

    private void OnRun(InputValue input)
    {
        bool isPressed = input.isPressed;

        SetRun(isPressed);
    }

    private void OnDrain(InputValue input)
    {
        bool isPressed = input.isPressed;
        SetDrain(isPressed);
    }

    private void SetMove(Vector2 value)
    {
        _input = value;
    }

    private void SetRun(bool isPressed)
    {
        isRun = isPressed;  
    }

    private void SetDrain(bool isPressed)
    {
        isDrain = isPressed;
    }
}
