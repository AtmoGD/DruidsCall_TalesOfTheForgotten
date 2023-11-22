using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Character
{
    #region Wolf States
    public WolfState Idle { get; private set; }
    public WolfState Running { get; private set; }
    public WolfState Jumping { get; private set; }
    public WolfState Falling { get; private set; }
    public WolfState TeleportToHero { get; private set; }
    public WolfState Attacking { get; private set; }
    public WolfState FollowHeroJump { get; private set; }
    public WolfState FollowHeroFalling { get; private set; }
    #endregion

    #region Wolf Settings
    [field: Header("Wolf Settings")]
    [field: SerializeField] public Hero Hero { get; private set; } = null;


    [field: Header("Follow Hero")]
    [field: SerializeField] public FollowTargetComponent FollowTargetComponent { get; private set; } = null;
    [field: SerializeField] public Transform FollowTransform { get; private set; } = null;
    [field: SerializeField] public float FollowRadius { get; private set; } = 1f;
    [field: SerializeField] public float TeleportRadius { get; private set; } = 2f;
    [field: SerializeField] public float GroundedDistance { get; private set; } = 1f;
    [field: SerializeField] public bool IncreaseSpeedBasedOnHeroDistance { get; private set; } = true;

    [field: Header("Teleporting")]
    [field: SerializeField] public Vector2 TeleportOffset { get; private set; } = Vector2.zero;


    [field: Header("Debugging")]
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;
    #endregion

    #region Character Variables
    [field: Header("Runtime Variables")]
    [field: SerializeField] public WolfInput CurrentInput { get; set; } = new WolfInput();

    #endregion

    public bool InFollowRadius
    => Vector2.Distance(transform.position, FollowTransform.position) > FollowRadius
    && Vector2.Distance(transform.position, FollowTransform.position) < TeleportRadius;

    public bool InTeleportRadius
    => Vector2.Distance(transform.position, FollowTransform.position) > TeleportRadius;

    public bool IsGroundedInMovementDirection
    => Physics2D.OverlapBox(GroundTransform.position + ((Vector3)CurrentInput.Move * GroundedDistance), GroundBoxSize, 0, GroundLayer);

    private void Awake()
    {
        Idle = new WolfIdle(this);
        Running = new WolfRunning(this);
        Jumping = new WolfJumping(this);
        Falling = new WolfFalling(this);
        TeleportToHero = new WolfTeleportToHero(this);
        Attacking = new WolfAttacking(this);
        FollowHeroJump = new WolfFollowHeroJump(this);
        FollowHeroFalling = new WolfFollowHeroFalling(this);
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

    public override bool Grounded()
    {
        if (base.Grounded())
        {
            JumpsLeft = MaxJumps;
            return true;
        }

        return false;
    }

    private new void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FollowRadius);
        Gizmos.DrawWireSphere(transform.position, TeleportRadius);
    }
}
