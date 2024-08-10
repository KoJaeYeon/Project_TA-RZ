using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [Header("InputSystem")]
    [SerializeField] private Vector2 _input;

    [Header("Dash")]
    [SerializeField] private bool isDash;
    [Header("Drain")]
    [SerializeField] private bool isDrain;
    [Header("Attack")]
    [SerializeField] private bool isAttack;
    [Header("Look")]
    [SerializeField] private float deltaLook;
    [Header("LockOn")]
    [SerializeField] private bool isLockOn;

    public Vector2 Input { get { return _input; } }
    public bool IsDrain { get { return isDrain; } }
    public bool IsDash { get { return isDash; } }
    public bool IsAttack { get {  return isAttack; } }
    public float DeltaLook { get { return deltaLook; } set { deltaLook = value; } }
    public bool IsLockOn { get { return isLockOn; } }

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

    private void OnAttack(InputValue input)
    {
        bool isPressed = input.isPressed;

        SetAttack(isPressed);
    }

    private void OnLook(InputValue input)
    {
        float delta = input.Get<float>();

        SetLook(delta);
    }

    private void OnLockOn(InputValue input)
    {
        bool isPressed = input.isPressed;

        SetLockOn(isPressed);
    }

    private void OnLockOnFix(InputValue input)
    {
        //Button입력으로 True값만 받음
        SetLockOn(!isLockOn);
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

    private void SetAttack(bool isPressed)
    {
        isAttack = isPressed;
    }

    private void SetLook(float delta)
    {
        deltaLook += delta;
    }

    private void SetLockOn(bool isPressed)
    {
        isLockOn = isPressed;
    }
}
