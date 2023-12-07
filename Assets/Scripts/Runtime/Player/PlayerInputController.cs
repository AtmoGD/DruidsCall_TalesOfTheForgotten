using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class HeroInput
{
    public bool Interact = false;
    public Vector2 Move = Vector2.zero;
    public int LastMoveDirection = 1;
    public bool Jump = false;
    public bool Attack = false;
}


public class PlayerInputController : MonoBehaviour
{
    public HeroInput HeroInput { get; protected set; } = new HeroInput();
    [field: SerializeField] public WolfInputController WolfInputController { get; private set; } = null;
    public WolfInput WolfInput => WolfInputController.WolfInput;

    #region Hero active
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

    public void OnCharacterInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            HeroInput.Interact = true;
        else if (context.canceled)
            HeroInput.Interact = false;
    }

    public void OnWolfCommandAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            WolfInput.Attack = true;
        else if (context.canceled)
            WolfInput.Attack = false;
    }
    #endregion

    #region Wolf active
    public void OnWolfMove(InputAction.CallbackContext context)
    {
        WolfInput.Move = context.ReadValue<Vector2>();

        if (WolfInput.Move.x > 0.1f)
            WolfInput.LastMoveDirection = 1;
        else if (WolfInput.Move.x < -0.1f)
            WolfInput.LastMoveDirection = -1;
    }

    public void OnWolfJump(InputAction.CallbackContext context)
    {
        if (context.started)
            WolfInput.Jump = true;
        else if (context.canceled)
            WolfInput.Jump = false;
    }
    #endregion
}