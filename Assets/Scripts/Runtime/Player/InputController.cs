using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class InputData
{
    public Vector2 move = Vector2.zero;
    public bool jump = false;
    public bool attack = false;
}

public class InputController : MonoBehaviour
{
    [field: SerializeField] public List<InputData> DataHistory { get; private set; } = new List<InputData>();
    public InputData Data => DataHistory[^1];
    private InputData _data = new();

    private void Start()
    {
        DataHistory.Add(new InputData());
    }

    private void Update()
    {
        DataHistory.Add(_data);
        _data = new InputData();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _data.move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            _data.jump = context.started || context.performed;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            _data.attack = context.started || context.performed;
    }
}
