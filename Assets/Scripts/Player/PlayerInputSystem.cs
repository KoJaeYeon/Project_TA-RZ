using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [Header("InputSystem")]
    [SerializeField] private Vector2 _input;

    [Header("Run")]
    [SerializeField] private bool isRun;
    [Header("Dash")]
    [SerializeField] private bool isDash;
    [Header("Drain")]
    [SerializeField] private bool isDrain;

    public Vector2 Input { get { return _input; } }
    public bool IsRun { get { return isRun; } }
    public bool IsDrain { get { return isDrain; } }
    public bool IsDash { get { return isDash; } }

    private void OnMove(InputValue input)
    {
        SetMove(input.Get<Vector2>());  
    }

    private void OnDash(InputValue input)
    {
        bool isPressed = input.isPressed;

        SetDash(isPressed);
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

    private void SetDash(bool isPressed)
    {
        isDash = isPressed; 
    }

    private void SetDrain(bool isPressed)
    {
        isDrain = isPressed;
    }
}
