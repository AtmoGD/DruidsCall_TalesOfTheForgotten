using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; } = null;
    [field: SerializeField] public PlayerInputController PlayerInputController { get; private set; }
    [field: SerializeField] public Niamh Niamh { get; private set; } = null;

    [field: SerializeField] public bool IsActive { get; private set; } = false;

    private void Start()
    {
        if (IsActive) Init();
    }

    public void Init()
    {
        Niamh.CurrentInput = PlayerInputController.NiamhInput;
        PlayerInput.SwitchCurrentActionMap("Niamh");
    }
}
