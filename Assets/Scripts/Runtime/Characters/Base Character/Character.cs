using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : StateMachine
{
    [field: Header("Character Settings")]
    [field: SerializeField] public bool IsActive { get; private set; } = true;
    [field: SerializeField] public bool IsControlledByPlayer { get; private set; } = false;

    [field: Header("Character References")]
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; } = null;
    [field: SerializeField] public Animator Animator { get; private set; } = null;
    [field: SerializeField] public Transform GroundTransform { get; private set; } = null;
    [field: SerializeField] public Transform TopTransform { get; private set; } = null;
    [field: SerializeField] public Transform WallLeftTransform { get; private set; } = null;
    [field: SerializeField] public Transform WallRightTransform { get; private set; } = null;

    [field: Header("Ground Check Parameters")]
    [field: SerializeField] public Vector2 GroundBoxSize { get; private set; } = Vector2.one;
    [field: SerializeField] public LayerMask GroundLayer { get; private set; } = 0;

    [field: Header("Top Check Parameters")]
    [field: SerializeField] public Vector2 TopBoxSize { get; private set; } = Vector2.one;
    [field: SerializeField] public LayerMask TopLayer { get; private set; } = 0;

    [field: Header("Wall Check Parameters")]
    [field: SerializeField] public Vector2 WallBoxSize { get; private set; } = Vector2.one;
    [field: SerializeField] public LayerMask WallLayer { get; private set; } = 0;

    [field: Header("Phase Through Check Parameters")]
    [field: SerializeField] public LayerMask PhaseThroughLayer { get; private set; } = 0;

    [field: Header("Movement")]
    [field: SerializeField] public bool MoveActive { get; private set; } = true;
    [field: SerializeField] public float MaxSpeed { get; private set; } = 1f;
    [field: SerializeField] public AnimationCurve AccelerationCurve { get; private set; } = null;
    [field: SerializeField] public bool ResetAccelerationOnDirectionChange { get; private set; } = true;
    [field: SerializeField] public AnimationCurve DeccelerationCurve { get; private set; } = null;

    [field: Header("Jumping")]
    [field: SerializeField] public bool JumpActive { get; private set; } = true;
    [field: SerializeField] public bool CanPhaseThroughPlatforms { get; private set; } = true;
    [field: SerializeField] public int MaxJumps { get; private set; } = 2;
    public bool CanJump => JumpsLeft > 0 && JumpActive;
    [field: SerializeField] public float JumpHeight { get; private set; } = 1f;
    [field: SerializeField] public float MinJumpTime { get; private set; } = 0.15f;
    [field: SerializeField] public AnimationCurve JumpCurve { get; private set; } = null;
    [field: SerializeField] public AnimationCurve FallCurve { get; private set; } = null;
    [field: SerializeField] public float FallLerpSpeed { get; private set; } = 0.1f;

    [field: SerializeField] public int JumpsLeft { get; set; } = 0;

    protected virtual void Start()
    {
        if (!IsActive) gameObject.SetActive(false);
    }

    public virtual bool Grounded()
    {
        return Physics2D.OverlapBox(GroundTransform.position, GroundBoxSize, 0, GroundLayer);
    }

    public virtual bool HitsTop()
    {
        return Physics2D.OverlapBox(TopTransform.position, TopBoxSize, 0, TopLayer);
    }

    public virtual void SetIsControlledByPlayer(bool _isControlledByPlayer)
    {
        IsControlledByPlayer = _isControlledByPlayer;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GroundTransform.position, GroundBoxSize);
        Gizmos.DrawWireCube(TopTransform.position, TopBoxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(WallLeftTransform.position, WallBoxSize);
        Gizmos.DrawWireCube(WallRightTransform.position, WallBoxSize);
    }
}
