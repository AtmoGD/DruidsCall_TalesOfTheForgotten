using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : StateMachine
{
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; } = null;
    [field: SerializeField] public Animator Animator { get; private set; } = null;
    [field: SerializeField] public InputData CurrentInput { get; set; } = new InputData();
}
