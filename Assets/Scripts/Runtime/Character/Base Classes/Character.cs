using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : StateMachine
{
    [field: Header("Input Data (Just for debugging)")]
    [field: SerializeField] public InputData CurrentInput { get; set; } = new InputData();


    [field: Header("Character References")]
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; } = null;
    [field: SerializeField] public Animator Animator { get; private set; } = null;
    [field: SerializeField] public Transform GroundTransform { get; private set; } = null;
    [field: SerializeField] public Transform TopTransform { get; private set; } = null;


    [field: Header("Ground Check Parameters")]
    [field: SerializeField] public Vector2 GroundBoxSize { get; private set; } = Vector2.one;
    [field: SerializeField] public LayerMask GroundLayer { get; private set; } = 0;

    // Using a method instead of a property because we want to override in derived classes
    public virtual bool Grounded() { return Physics2D.OverlapBox(GroundTransform.position, GroundBoxSize, 0, GroundLayer); }


    [field: Header("Top Check Parameters")]
    [field: SerializeField] public Vector2 TopBoxSize { get; private set; } = Vector2.one;
    [field: SerializeField] public LayerMask TopLayer { get; private set; } = 0;

    // Using a method instead of a property because we want to override it in derived classes
    public virtual bool HitsTop() { return Physics2D.OverlapBox(TopTransform.position, TopBoxSize, 0, TopLayer); }


    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GroundTransform.position, GroundBoxSize);
        Gizmos.DrawWireCube(TopTransform.position, TopBoxSize);
    }
}

