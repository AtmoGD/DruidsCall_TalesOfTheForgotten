using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class InputData
{
    public Vector2 Move = Vector2.zero;
    public bool Jump = false;
    public bool Attack = false;
}

public class InputController : MonoBehaviour
{
    [field: SerializeField] public List<InputData> DataHistory { get; private set; } = new List<InputData>();
    // public InputData Data => DataHistory[^1];
    public InputData Data => _data;
    private InputData _data = new();

    private void Start()
    {
        // DataHistory.Add(new InputData());
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
        // DataHistory.Add(_data);
        // _data = new InputData();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _data.Move = context.ReadValue<Vector2>();
        // print(_data.Move);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            _data.Jump = context.started || context.performed;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            _data.Attack = context.started || context.performed;
    }
}
