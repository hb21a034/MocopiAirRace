using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TestProCon : MonoBehaviour
{
    public static float Throttle { get; private set; }

    public void GetThrottleValue(InputAction.CallbackContext context)
    {
        Throttle = context.ReadValue<float>();
    }

    void Update()
    {
        // Debug.Log(Throttle);
    }
}