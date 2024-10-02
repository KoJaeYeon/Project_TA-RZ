using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereMove : MonoBehaviour
{
    private void OnMove(InputValue inputValue)
    {
        Debug.Log(inputValue);
    }

    private void OnFire(InputValue inputValue)
    {
        Debug.Log(inputValue.isPressed);
        
    }
}
