using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : Character
{
    #region Events
    public UnityEvent onJump = new UnityEvent();
    #endregion

    #region Character States
    public HeroState Idle { get; private set; }
    public HeroState Running { get; private set; }
    public HeroState Jumping { get; private set; }
    public HeroWallJump WallJump { get; private set; }
    public HeroState Falling { get; private set; }
    public HeroState Attacking { get; private set; }
    #endregion

    #region Character Components
    [field: Header("Character Components")]
    [field: SerializeField] public DirectionComponent DirectionComponent { get; private set; } = null;
    [field: SerializeField] public CooldownComponent CooldownComponent { get; private set; } = null;
    #endregion


    #region Hero Settings
    [field: Header("Hero Settings")]
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;
    [field: SerializeField] public Finn Finn { get; private set; } = null;
    [field: SerializeField] public Transform Follow { get; private set; } = null;

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


    [field: Header("Debugging")]
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;
    #endregion

    #region Hero Variables
    [field: Header("Runtime Variables")]
    [field: SerializeField] public HeroInput CurrentInput { get; set; } = new HeroInput();

    #endregion

    #region Hero Skill Variables
    [field: Header("Skill Variables")]
    [field: SerializeField] public bool WallJumpResetsJumps { get; private set; } = true;
    #endregion

    private void Awake()
    {
        Idle = new HeroIdle(this);
        Running = new HeroRunning(this);
        Jumping = new HeroJumping(this);
        WallJump = new HeroWallJump(this);
        Falling = new HeroFalling(this);
        Attacking = new HeroAttacking(this);
    }

    protected new void Start()
    {
        base.Start();

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

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.cyan;

        // Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
        Gizmos.DrawWireSphere(AttackStartPoint.position, AttackRadius);
        Gizmos.DrawWireSphere(AttackEndPoint.position, AttackRadius);

        if (WallJump == null) return;

        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + (Vector3.right * WallJump.wallJumpDirection.x * 0.5f) + Vector3.up);
        Gizmos.DrawWireSphere(transform.position + (Vector3.right * WallJump.wallJumpDirection.x * 0.5f) + Vector3.up, 0.1f);
    }
}
