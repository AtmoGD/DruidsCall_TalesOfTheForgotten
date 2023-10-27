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
    #endregion

    #region Wolf Settings
    [field: Header("Wolf Settings")]
    [field: SerializeField] public Hero Hero { get; private set; } = null;


    [field: Header("Follow Player")]
    [field: SerializeField] public float FollowRadius { get; private set; } = 1f;
    [field: SerializeField] public float TeleportRadius { get; private set; } = 2f;


    [field: Header("Debugging")]
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;
    #endregion

    #region Character Variables
    [field: Header("Runtime Variables")]
    [field: SerializeField] public WolfInput CurrentInput { get; set; } = new WolfInput();
    // [field: SerializeField] public int JumpsLeft { get; set; } = 0;

    #endregion

    public bool InFollowRadius
    => Vector2.Distance(transform.position, Hero.transform.position) > FollowRadius
    && Vector2.Distance(transform.position, Hero.transform.position) < TeleportRadius;

    public bool CharacterInTeleportRadius
    => Vector2.Distance(transform.position, Hero.transform.position) < TeleportRadius;

    private void Awake()
    {
        Idle = new WolfIdle(this);
        Running = new WolfRunning(this);
        Jumping = new WolfJumping(this);
        Falling = new WolfFalling(this);
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
