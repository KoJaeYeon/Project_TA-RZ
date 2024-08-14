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
    [Header("AttackCount")]
    [SerializeField] private int attackCount;
    [Header("Look")]
    [SerializeField] private float deltaLook;
    [Header("LockOn")]
    [SerializeField] private bool isLockOn;
    [Header("Skill")]
    [SerializeField] private bool isSkill;

    public Vector2 Input { get { return _input; } }
    public bool IsDrain { get { return isDrain; } }
    public bool IsDash { get { return isDash; } }
    public bool IsAttack { get { return isAttack; } }
    public int AttackCount {  get { return attackCount; } set { attackCount = value; } }
    public float DeltaLook { get { return deltaLook; } set { deltaLook = value; } }
    public bool IsLockOn { get { return isLockOn; } }
    public bool IsSkill { get { return isSkill; } }

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

        //if (isPressed)
        //{
        //    attackCount++;

        //    Debug.Log(attackCount);
        //}
        
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

    private void OnSkill(InputValue input)
    {
        bool isPressed = input.isPressed;

        SetSkill(isPressed);
    }

    private void SetMove(Vector2 value)
    {
        _input = value;
    }

    public void SetDash(bool isPressed)
    {
        isDash = isPressed; 
    }

    public void SetDrain(bool isPressed)
    {
        isDrain = isPressed;
    }

    public void SetAttack(bool isPressed)
    {
        isAttack = isPressed;
    }

    private void SetLook(float delta)
    {
        deltaLook += delta;
    }

    public void SetLockOn(bool isPressed)
    {
        isLockOn = isPressed;
    }

    private void SetSkill(bool isPressed)
    {
        isSkill = isPressed;    
    }
}
