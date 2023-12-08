using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public Character Character { get; private set; } = null;
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; } = null;
    [field: SerializeField] public PlayerInputController PlayerInputController { get; private set; }
    [field: SerializeField] public Niamh Niamh { get; private set; } = null;
    [field: SerializeField] public WolfInputController WolfInputController { get; private set; } = null;
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;

    [field: SerializeField] public bool IsActive { get; private set; } = false;

    private void Start()
    {
        if (IsActive) Init();
    }

    public void Init()
    {
        Niamh.CurrentInput = PlayerInputController.NiamhInput;
        Wolf.CurrentInput = WolfInputController.WolfInput;
        PlayerInput.SwitchCurrentActionMap("Niamh");
    }

    // public void SwitchCharacter()
    // {
    //     if (Character == Niamh)
    //     {
    //         Character = Wolf;
    //         // Wolf.CurrentInput = PlayerInputController.WolfInput;
    //         Wolf.SetIsControlledByPlayer(true);
    //         Niamh.SetIsControlledByPlayer(false);
    //         Niamh.CurrentInput = new NiamhInput();
    //         PlayerInput.SwitchCurrentActionMap("Wolf");
    //     }
    //     else
    //     {
    //         Character = Niamh;
    //         Niamh.CurrentInput = PlayerInputController.NiamhInput;
    //         Niamh.SetIsControlledByPlayer(true);
    //         Wolf.SetIsControlledByPlayer(false);
    //         // Wolf.CurrentInput = WolfInputController.WolfInput;
    //         PlayerInput.SwitchCurrentActionMap("Niamh");
    //     }
    // }
}
