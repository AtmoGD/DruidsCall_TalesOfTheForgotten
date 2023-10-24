using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class HeroInput
{
    public Vector2 Move = Vector2.zero;
    public int LastMoveDirection = 1;
    public bool Jump = false;
    public bool Attack = false;
}

[Serializable]
public class WolfInput
{
    public Vector2 Move = Vector2.zero;
    public int LastMoveDirection = 1;
    public bool Jump = false;
    public bool Attack = false;
}


public class PlayerInputController : MonoBehaviour
{
    public HeroInput HeroInput { get; protected set; } = new HeroInput();
    public WolfInput WolfInput { get; protected set; } = new WolfInput();

    public void OnCharacterMove(InputAction.CallbackContext context)
    {
        HeroInput.Move = context.ReadValue<Vector2>();

        if (HeroInput.Move.x > 0.1f)
            HeroInput.LastMoveDirection = 1;
        else if (HeroInput.Move.x < -0.1f)
            HeroInput.LastMoveDirection = -1;
    }

    // TODO: Implement Jump Buffering
    public void OnCharacterJump(InputAction.CallbackContext context)
    {
        if (context.started)
            HeroInput.Jump = true;
        else if (context.canceled)
            HeroInput.Jump = false;
    }

    public void OnCharacterAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            HeroInput.Attack = true;
        else if (context.canceled)
            HeroInput.Attack = false;
    }
}