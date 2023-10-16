using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : StateMachine
{
    [field: Header("Character References")]
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; } = null;
    [field: SerializeField] public Animator Animator { get; private set; } = null;

    [field: Header("Input Data (Just for debugging)")]
    [field: SerializeField] public InputData CurrentInput { get; set; } = new InputData();
}
