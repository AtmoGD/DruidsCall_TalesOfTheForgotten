using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : StateMachine
{
    [field: SerializeField] public bool IsActive { get; private set; } = true;
    [field: SerializeField] public bool IsControlledByPlayer { get; private set; } = false;

    [field: Header("Events")]
    public UnityEvent OnJump { get; private set; } = new UnityEvent();

    [field: Header("References")]
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;
    [field: SerializeField] public Finn Finn { get; private set; } = null;
    [field: SerializeField] public Transform Follow { get; private set; } = null;
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; } = null;
    [field: SerializeField] public Animator Animator { get; private set; } = null;
    [field: SerializeField] public Transform SkinHolder { get; private set; } = null;
    [field: SerializeField] public Transform GroundTransform { get; private set; } = null;
    [field: SerializeField] public Transform TopTransform { get; private set; } = null;
    [field: SerializeField] public Transform WallLeftTransform { get; private set; } = null;
    [field: SerializeField] public Transform WallRightTransform { get; private set; } = null;

    [field: Header("Components")]
    [field: SerializeField] public DirectionComponent DirectionComponent { get; private set; } = null;
    [field: SerializeField] public CooldownComponent CooldownComponent { get; private set; } = null;

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

    [field: Header("Wall Jump")]
    [field: SerializeField] public bool WallJumpActive { get; private set; } = true;
    public bool CanWallJump => WallJumpActive && (WallJumpConsumesJump ? JumpsLeft > 0 : true);
    [field: SerializeField] public bool WallJumpConsumesJump { get; private set; } = true;
    [field: SerializeField] public AnimationCurve WallJumpCurve { get; private set; } = null;

    [field: Header("Falling")]
    [field: SerializeField] public AnimationCurve FallCurve { get; private set; } = null;
    [field: SerializeField] public float FallLerpSpeed { get; private set; } = 0.1f;
    [field: SerializeField] public float IdleGravity { get; private set; } = -1f;

    [field: Header("Fall Through Platform")]
    [field: SerializeField] public bool FallThroughPlatformActive { get; private set; } = true;
    public bool CanFallThroughPlatform => FallThroughPlatformActive && CanPhaseThroughPlatforms;
    [field: SerializeField] public float FallThroughPlatformTime { get; private set; } = 0.1f;
    [field: SerializeField] public float FallThroughPlatformThreshold { get; private set; } = -0.9f;

    [field: Header("Attack Settings")]
    [field: SerializeField] public bool AttackActive { get; private set; } = true;
    [field: SerializeField] public string AttackName { get; private set; } = "Attack";
    public bool CanAttack => AttackActive && !CooldownComponent.HasCooldown(AttackName);
    [field: SerializeField] public Transform AttackStartPoint { get; private set; } = null;
    [field: SerializeField] public Transform AttackEndPoint { get; private set; } = null;
    [field: SerializeField] public float AttackRadius { get; private set; } = 0.5f;
    [field: SerializeField] public int AttackDamage { get; private set; } = 10;
    [field: SerializeField] public float AttackCooldown { get; private set; } = 0.2f;
    [field: SerializeField] public float AttackTime { get; private set; } = 0.7f;
    [field: SerializeField] public float AttackStopLerpSpeed { get; private set; } = 50f;

    [field: Header("Skill Variables")]
    [field: SerializeField] public bool WallJumpResetsJumps { get; private set; } = true;

    [field: Header("Debugging")]
    [field: SerializeField] public bool ShowDebugLogs { get; private set; } = true;
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;


    [field: Header("Runtime Variables")]
    [field: SerializeField] public int JumpsLeft { get; set; } = 0;
    [field: SerializeField] public HeroInput CurrentInput { get; set; } = new HeroInput();

    public HeroState Idle { get; private set; }
    public HeroState Running { get; private set; }
    public HeroState Jumping { get; private set; }
    public HeroWallJump WallJump { get; private set; }
    public HeroState Falling { get; private set; }
    public HeroState Attacking { get; private set; }


    private void Awake()
    {
        Idle = new HeroIdle(this);
        Running = new HeroRunning(this);
        Jumping = new HeroJumping(this);
        WallJump = new HeroWallJump(this);
        Falling = new HeroFalling(this);
        Attacking = new HeroAttacking(this);
    }

    protected virtual void Start()
    {
        if (!IsActive) gameObject.SetActive(false);

        ChangeState(Idle);
    }

    protected override void Update()
    {
        base.Update();

        StateText.text = CurrentState.GetType().Name;
    }

    public override void ChangeState(State _newState)
    {
        base.ChangeState(_newState);

        if (ShowDebugLogs) Debug.Log($"<color=green>Hero</color> changed state to <color=yellow>{CurrentState.GetType().Name}</color>");
    }

    public virtual bool Grounded()
    {
        return Physics2D.OverlapBox(GroundTransform.position, GroundBoxSize, 0, GroundLayer);
    }

    public virtual void ResetJumpsLeft()
    {
        JumpsLeft = MaxJumps;
    }

    public virtual bool OnPlatform()
    {
        return Physics2D.OverlapBox(GroundTransform.position, GroundBoxSize, 0, PhaseThroughLayer);
    }

    public virtual bool HitsTop()
    {
        return Physics2D.OverlapBox(TopTransform.position, TopBoxSize, 0, TopLayer);
    }

    public virtual bool HitsWallLeft()
    {
        return Physics2D.OverlapBox(WallLeftTransform.position, WallBoxSize, 0, WallLayer);
    }

    public virtual bool HitsWallRight()
    {
        return Physics2D.OverlapBox(WallRightTransform.position, WallBoxSize, 0, WallLayer);
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

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(AttackStartPoint.position, AttackRadius);
        Gizmos.DrawWireSphere(AttackEndPoint.position, AttackRadius);

        if (WallJump == null) return; // The walljump state only exists at runtime

        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + (Vector3.right * WallJump.wallJumpDirection.x * 0.5f) + Vector3.up);
        Gizmos.DrawWireSphere(transform.position + (Vector3.right * WallJump.wallJumpDirection.x * 0.5f) + Vector3.up, 0.1f);
    }
}
