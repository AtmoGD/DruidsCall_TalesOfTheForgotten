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
    #endregion


    #region Hero Settings
    [field: Header("Hero Settings")]
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;


    [field: Header("Debugging")]
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;
    #endregion

    #region Hero Variables
    [field: Header("Runtime Variables")]
    [field: SerializeField] public HeroInput CurrentInput { get; set; } = new HeroInput();

    #endregion

    #region Hero Skill Variables
    [field: SerializeField] public bool WallJumpResetsJumps { get; private set; } = true;
    #endregion

    private void Awake()
    {
        Idle = new HeroIdle(this);
        Running = new HeroRunning(this);
        Jumping = new HeroJumping(this);
        WallJump = new HeroWallJump(this);
        Falling = new HeroFalling(this);
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

        if (WallJump == null) return;

        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + (Vector3.right * WallJump.wallJumpDirection.x * 0.5f) + Vector3.up);
        Gizmos.DrawWireSphere(transform.position + (Vector3.right * WallJump.wallJumpDirection.x * 0.5f) + Vector3.up, 0.1f);
        // Gizmos.DrawRay(transform.position, WallJump.wallJumpDirection * 0.5f);
    }
}
