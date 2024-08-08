using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [Header("InputSystem")]
    [SerializeField] private Vector2 _input;

    [Header("Dash")]
    [SerializeField] private bool isDash;

    public Vector2 Input { get { return _input; } }
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

    private void SetMove(Vector2 value)
    {
        _input = value;
    }

    private void SetDash(bool isPressed)
    {
        isDash = isPressed; 
    }
}
