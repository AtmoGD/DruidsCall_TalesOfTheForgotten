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
    public HeroState Falling { get; private set; }
    #endregion


    #region Character Settings
    [field: Header("Main Character Settings")]
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;


    [field: Header("Wall Jump")]
    [field: SerializeField] public bool WallJumpActive { get; private set; } = true;
    [field: SerializeField] public bool WallJumpConsumesJump { get; private set; } = true;


    [field: Header("Debugging")]
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;
    #endregion

    #region Character Variables
    [field: Header("Runtime Variables")]
    [field: SerializeField] public HeroInput CurrentInput { get; set; } = new HeroInput();

    #endregion

    private void Awake()
    {
        Idle = new HeroIdle(this);
        Running = new HeroRunning(this);
        Jumping = new HeroJumping(this);
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

    public override bool Grounded()
    {
        if (base.Grounded())
        {
            JumpsLeft = MaxJumps;
            return true;
        }

        return false;
    }
}
