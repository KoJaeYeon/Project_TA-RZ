using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [Header("InputSystem")]
    [SerializeField] private Vector2 _input;

    [Header("Run")]
    [SerializeField] private bool isRun;

    public Vector2 Input { get { return _input; } }
    public bool IsRun { get { return isRun; } }

    private void OnMove(InputValue input)
    {
        SetMove(input.Get<Vector2>());  
    }

    private void OnRun(InputValue input)
    {
        bool isPressed = input.isPressed;

        SetRun(isPressed);
    }

    private void SetMove(Vector2 value)
    {
        _input = value;
    }

    private void SetRun(bool isPressed)
    {
        isRun = isPressed;  
    }
}
