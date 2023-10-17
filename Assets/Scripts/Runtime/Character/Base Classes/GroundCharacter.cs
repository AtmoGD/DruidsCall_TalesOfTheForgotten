using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCharacter : Character
{
    [field: Header("Ground Check Parameters")]
    [field: SerializeField] public Vector2 GroundedBoxSize { get; private set; } = Vector2.one;
    [field: SerializeField] public Vector2 GroundedBoxOffset { get; private set; } = Vector2.zero;
    [field: SerializeField] public LayerMask GroundLayer { get; private set; } = 0;
    // public bool IsGrounded => Physics2D.OverlapBox(transform.position + (Vector3)GroundedBoxOffset, GroundedBoxSize, 0, GroundLayer);
    public virtual bool IsGrounded()
    {
        return Physics2D.OverlapBox(transform.position + (Vector3)GroundedBoxOffset, GroundedBoxSize, 0, GroundLayer);
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)GroundedBoxOffset, GroundedBoxSize);
    }
}
