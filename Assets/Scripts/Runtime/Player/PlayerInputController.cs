using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class NiamhInput
{
    public bool Interact = false;
    public Vector2 Move = Vector2.zero;
    public int LastMoveDirection = 1;
    public bool Jump = false;
    public bool Attack = false;
    public bool Dash = false;
    public bool Glide = false;
}


public class PlayerInputController : MonoBehaviour
{
    public NiamhInput NiamhInput { get; protected set; } = new NiamhInput();
    #region Niamh active

    public void OnPause(InputAction.CallbackContext _context)
    {
        if (_context.started)
            Game.Manager.Player.Pause();
    }

    public void OnCharacterMove(InputAction.CallbackContext _context)
    {
        NiamhInput.Move = _context.ReadValue<Vector2>();

        if (NiamhInput.Move.x > 0.1f)
            NiamhInput.LastMoveDirection = 1;
        else if (NiamhInput.Move.x < -0.1f)
            NiamhInput.LastMoveDirection = -1;
    }

    // TODO: Implement Jump Buffering
    public void OnCharacterJump(InputAction.CallbackContext _context)
    {
        if (_context.started)
            NiamhInput.Jump = true;
        else if (_context.canceled)
            NiamhInput.Jump = false;
    }

    public void OnCharacterAttack(InputAction.CallbackContext _context)
    {
        if (_context.started)
            NiamhInput.Attack = true;
        else if (_context.canceled)
            NiamhInput.Attack = false;
    }

    public void OnCharacterDash(InputAction.CallbackContext _context)
    {
        if (_context.started)
            NiamhInput.Dash = true;
        else if (_context.canceled)
            NiamhInput.Dash = false;
    }

    public void OnCharacterInteract(InputAction.CallbackContext _context)
    {
        if (_context.started)
            NiamhInput.Interact = true;
        else if (_context.canceled)
            NiamhInput.Interact = false;
    }

    public void OnCharacterGlide(InputAction.CallbackContext _context)
    {
        if (_context.started)
            NiamhInput.Glide = true;
        else if (_context.canceled)
            NiamhInput.Glide = false;
    }
    #endregion
}