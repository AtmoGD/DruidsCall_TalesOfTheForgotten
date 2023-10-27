using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public Character Character { get; private set; } = null;
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; } = null;
    [field: SerializeField] public PlayerInputController PlayerInputController { get; private set; }
    [field: SerializeField] public Hero Hero { get; private set; } = null;
    [field: SerializeField] public WolfInputController WolfInputController { get; private set; } = null;
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;

    [field: SerializeField] public bool IsActive { get; private set; } = false;

    private void Start()
    {
        if (IsActive) Init();
    }

    public void Init()
    {
        Hero.CurrentInput = PlayerInputController.HeroInput;
        Wolf.CurrentInput = WolfInputController.WolfInput;
        PlayerInput.SwitchCurrentActionMap("Hero");
    }

    public void SwitchCharacter()
    {
        if (Character == Hero)
        {
            Character = Wolf;
            Wolf.CurrentInput = PlayerInputController.WolfInput;
            Wolf.SetIsControlledByPlayer(true);
            Hero.SetIsControlledByPlayer(false);
            Hero.CurrentInput = new HeroInput();
            PlayerInput.SwitchCurrentActionMap("Wolf");
        }
        else
        {
            Character = Hero;
            Hero.CurrentInput = PlayerInputController.HeroInput;
            Hero.SetIsControlledByPlayer(true);
            Wolf.SetIsControlledByPlayer(false);
            Wolf.CurrentInput = WolfInputController.WolfInput;
            PlayerInput.SwitchCurrentActionMap("Hero");
        }
    }
}
