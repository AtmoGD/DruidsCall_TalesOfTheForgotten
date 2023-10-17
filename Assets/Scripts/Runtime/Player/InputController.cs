using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class InputData
{
    public Vector2 Move = Vector2.zero;
    public int LastMoveDirection = 1;
    public bool Jump = false;
    public bool Attack = false;
}

public class InputController : MonoBehaviour
{
    public InputData Data { get; private set; } = new InputData();

    public void OnMove(InputAction.CallbackContext context)
    {
        Data.Move = context.ReadValue<Vector2>();

        if (Data.Move.x > 0.1f)
            Data.LastMoveDirection = 1;
        else if (Data.Move.x < -0.1f)
            Data.LastMoveDirection = -1;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            Data.Jump = true;
        else if (context.canceled)
            Data.Jump = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            Data.Attack = true;
        else if (context.canceled)
            Data.Attack = false;
    }
}
