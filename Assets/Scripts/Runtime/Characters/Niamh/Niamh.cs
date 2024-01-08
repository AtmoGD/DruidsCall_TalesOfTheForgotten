using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MoreMountains.Feedbacks;

public class Niamh : StateMachine
{
    [field: SerializeField] public bool IsActive { get; private set; } = true;

    [field: Header("Events")]
    public UnityEvent OnJump { get; private set; } = new UnityEvent();

    [field: Header("References")]
    [field: SerializeField] public Finn Finn { get; private set; } = null;
    [field: SerializeField] public Transform Follow { get; private set; } = null;
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; } = null;
    [field: SerializeField] public Collider2D Collider { get; private set; } = null;
    [field: SerializeField] public Animator Animator { get; private set; } = null;
    [field: SerializeField] public Transform SkinHolder { get; private set; } = null;
    [field: SerializeField] public Transform GroundTransform { get; private set; } = null;
    [field: SerializeField] public Transform TopTransform { get; private set; } = null;
    [field: SerializeField] public Transform WallLeftTransform { get; private set; } = null;
    [field: SerializeField] public Transform WallRightTransform { get; private set; } = null;

    [field: Header("Components")]
    [field: SerializeField] public DirectionComponent DirectionComponent { get; private set; } = null;
    [field: SerializeField] public CooldownComponent CooldownComponent { get; private set; } = null;
    [field: SerializeField] public HealthComponent HealthComponent { get; private set; } = null;
    [field: SerializeField] public InteractionComponent InteractionComponent { get; private set; } = null;

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

    [field: Header("Teleport")]
    [field: SerializeField] public bool TeleportActive { get; private set; } = true;
    [field: SerializeField] public string TeleportName { get; private set; } = "Teleport";
    [field: SerializeField] public float TeleportCooldown { get; private set; } = 1f;

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

    [field: Header("Gliding")]
    [field: SerializeField] public bool GlideActive { get; private set; } = true;
    [field: SerializeField] public string GlideName { get; private set; } = "Glide";
    public bool CanGlide => GlideActive && !CooldownComponent.HasCooldown(GlideName);
    [field: SerializeField] public float GlideSpeed { get; private set; } = 1f;
    [field: SerializeField] public float GlideCooldown { get; private set; } = 0.2f;
    [field: SerializeField] public LayerMask AirflowLayer { get; private set; } = 0;

    [field: Header("Fall Through Platform")]
    [field: SerializeField] public bool FallThroughPlatformActive { get; private set; } = true;
    public bool CanFallThroughPlatform => FallThroughPlatformActive && CanPhaseThroughPlatforms;
    [field: SerializeField] public float FallThroughPlatformTime { get; private set; } = 0.1f;
    [field: SerializeField] public float FallThroughPlatformThreshold { get; private set; } = -0.9f;

    [field: Header("Get Hit")]
    [field: SerializeField] public bool GetHitActive { get; private set; } = true;
    [field: SerializeField] public string GetHitName { get; private set; } = "Get Hit";
    public bool CanGetHit => GetHitActive && !CooldownComponent.HasCooldown(GetHitName);
    [field: SerializeField] public float GetHitTime { get; private set; } = 0.5f;
    [field: SerializeField] public float GetHitCooldown { get; private set; } = 1f;
    [field: SerializeField] public float GetHitGravity { get; private set; } = 7f;

    [field: Header("Attack")]
    [field: SerializeField] public bool AttackActive { get; private set; } = true;
    [field: SerializeField] public string AttackName { get; private set; } = "Attack";
    public bool CanAttack => AttackActive && !CooldownComponent.HasCooldown(AttackName);
    [field: SerializeField] public Transform AttackStartPoint { get; private set; } = null;
    [field: SerializeField] public Transform AttackEndPoint { get; private set; } = null;
    [field: SerializeField] public float AttackRadius { get; private set; } = 0.5f;
    [field: SerializeField] public int AttackDamage { get; private set; } = 10;
    [field: SerializeField] public float AttackCooldown { get; private set; } = 0.2f;
    [field: SerializeField] public float AttackDoDamageTime { get; private set; } = 0.4f;
    [field: SerializeField] public float AttackEndTime { get; private set; } = 0.7f;
    [field: SerializeField] public float AttackStopLerpSpeed { get; private set; } = 50f;

    [field: Header("Dashing")]
    [field: SerializeField] public bool DashActive { get; private set; } = true;
    [field: SerializeField] public string DashName { get; private set; } = "Dash";
    public bool CanDash => DashActive && !CooldownComponent.HasCooldown(DashName);
    [field: SerializeField] public Transform DashCheckTransform { get; private set; } = null;
    [field: SerializeField] public Vector2 DashBoxSize { get; private set; } = Vector2.one;
    [field: SerializeField] public AnimationCurve DashCurve { get; private set; } = null;
    [field: SerializeField] public float DashSpeed { get; private set; } = 1f;
    [field: SerializeField] public float DashTime { get; private set; } = 0.2f;
    [field: SerializeField] public float DashCooldown { get; private set; } = 0.2f;
    [field: SerializeField] public int DashDamage { get; private set; } = 10;

    [field: Header("Dying")]
    [field: SerializeField] public float DyingTime { get; private set; } = 2.5f;
    [field: SerializeField] public float DyingResetTime { get; private set; } = 1f;

    [field: Header("Charged Attack")]
    [field: SerializeField] public bool ChargedAttackActive { get; private set; } = true;
    [field: SerializeField] public string ChargedAttackName { get; private set; } = "Charged Attack";
    public bool CanChargedAttack => ChargedAttackActive && !CooldownComponent.HasCooldown(ChargedAttackName);
    [field: SerializeField] public float ChargedAttackTimeMin { get; private set; } = 1f;
    [field: SerializeField] public float ChargedAttackTimeMax { get; private set; } = 2f;
    [field: SerializeField] public float ChargedAttackDamageMultiplier { get; private set; } = 2f;
    [field: SerializeField] public float ChargedAttackCooldown { get; private set; } = 0.2f;

    [field: Header("Feedbacks")]
    [field: SerializeField] public MMF_Player JumpFeedbacks { get; set; } = null;
    [field: SerializeField] public MMF_Player GetHitFeedbacks { get; set; } = null;
    [field: SerializeField] public MMF_Player DieFeedbacks { get; set; } = null;
    [field: SerializeField] public MMF_Player ChargingAttackFeedbacks { get; set; } = null;
    [field: SerializeField] public MMF_Player AttackFeedbacks { get; set; } = null;
    [field: SerializeField] public MMF_Player ChargedAttackFeedbacks { get; set; } = null;
    [field: SerializeField] public MMF_Player FootstepFeedbacks { get; set; } = null;

    [field: Header("Skill Variables")]
    [field: SerializeField] public bool WallJumpResetsJumps { get; private set; } = true;

    [field: Header("Debugging")]
    [field: SerializeField] public bool ShowDebugLogs { get; private set; } = true;
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;

    [field: Header("Runtime Variables")]
    [field: SerializeField] public int JumpsLeft { get; set; } = 0;
    [field: SerializeField] public NiamhInput CurrentInput { get; set; } = new NiamhInput();

    public NiamhState Idle { get; private set; }
    public NiamhState Running { get; private set; }
    public NiamhState Jumping { get; private set; }
    public NiamhWallJump WallJump { get; private set; }
    public NiamhState Falling { get; private set; }
    public NiamhGliding Gliding { get; private set; }
    public NiamhGetHit GetHit { get; private set; }
    public NiamhState Dying { get; private set; }
    public NiamhChargingAttack ChargingAttack { get; private set; }
    public NiamhState Attacking { get; private set; }
    public NiamhChargedAttacking ChargedAttack { get; private set; }
    public NiamhState Dashing { get; private set; }

    private void Awake()
    {
        Idle = new NiamhIdle(this);
        Running = new NiamhRunning(this);
        Jumping = new NiamhJumping(this);
        WallJump = new NiamhWallJump(this);
        Falling = new NiamhFalling(this);
        Gliding = new NiamhGliding(this);
        GetHit = new NiamhGetHit(this);
        Dying = new NiamhDying(this);
        ChargingAttack = new NiamhChargingAttack(this);
        Attacking = new NiamhAttacking(this);
        ChargedAttack = new NiamhChargedAttacking(this);
        Dashing = new NiamhDashing(this);
    }

    protected virtual void Start()
    {
        if (!IsActive) gameObject.SetActive(false);

        ChangeState(Idle);
    }

    private void OnEnable()
    {
        HealthComponent.OnTakeDamage.AddListener(OnTakeDamage);
        HealthComponent.OnDeath.AddListener(Die);
    }

    private void OnDisable()
    {
        HealthComponent.OnTakeDamage.RemoveListener(OnTakeDamage);
        HealthComponent.OnDeath.RemoveListener(Die);
    }

    public void Init()
    {
        HealthComponent.Reset();

        ChangeState(Idle);

        Rigidbody.velocity = new Vector2(0, IdleGravity);
    }

    protected override void Update()
    {
        if (!IsActive) return;

        base.Update();

        StateText.text = CurrentState.GetType().Name;
    }

    protected override void FixedUpdate()
    {
        if (!IsActive) return;

        base.FixedUpdate();
    }

    public override void ChangeState(State _newState)
    {
        base.ChangeState(_newState);

        if (ShowDebugLogs) Debug.Log($"<color=green>Niamh</color> changed state to <color=yellow>{CurrentState.GetType().Name}</color>");
    }

    public virtual void OnTakeDamage(Damage _damage)
    {
        if (CanGetHit)
        {
            GetHit.Damage = _damage;
            ChangeState(GetHit);
        }
    }

    public virtual void Die()
    {
        if (CurrentState != Dying)
            ChangeState(Dying);
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

    public virtual void PlayFootstepFeedback()
    {
        FootstepFeedbacks?.PlayFeedbacks();
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

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(DashCheckTransform.position, DashBoxSize);

        if (WallJump == null) return; // The walljump state only exists at runtime

        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + (Vector3.right * WallJump.wallJumpDirection.x * 0.5f) + Vector3.up);
        Gizmos.DrawWireSphere(transform.position + (Vector3.right * WallJump.wallJumpDirection.x * 0.5f) + Vector3.up, 0.1f);
    }
}
