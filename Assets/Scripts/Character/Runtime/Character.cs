using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : StateMachine
{
    public CharacterState Idle { get; private set; }
    public CharacterState Falling { get; private set; }
    public CharacterState Landing { get; private set; }

    [field: SerializeField] public AnimationCurve FallCurve { get; private set; } = null;


    [field: SerializeField] public Vector2 GroundedBoxSize { get; private set; } = Vector2.one;
    [field: SerializeField] public Vector2 GroundedBoxOffset { get; private set; } = Vector2.zero;
    [field: SerializeField] public LayerMask GroundLayer { get; private set; } = 0;
    public bool IsGrounded => Physics2D.OverlapBox(transform.position + (Vector3)GroundedBoxOffset, GroundedBoxSize, 0, GroundLayer);

    private void Awake()
    {
        Idle = new CharacterIdle(this);
        Falling = new CharacterFalling(this);
        Landing = new CharacterLanding(this);
    }

    private void Start()
    {
        ChangeState(Idle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)GroundedBoxOffset, GroundedBoxSize);
    }
}
