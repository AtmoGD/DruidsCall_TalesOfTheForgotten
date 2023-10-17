using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : GroundCharacter
{
    #region Wolf States
    public WolfState Idle { get; private set; }
    public WolfState Running { get; private set; }
    public WolfState Jumping { get; private set; }
    public WolfState Falling { get; private set; }
    #endregion

    #region Wolf Settings
    [field: Header("Movement")]
    [field: SerializeField] public bool MoveActive { get; private set; } = true;
    [field: SerializeField] public float MaxSpeed { get; private set; } = 1f;
    [field: SerializeField] public AnimationCurve AccelerationCurve { get; private set; } = null;
    [field: SerializeField] public bool ResetAccelerationOnDirectionChange { get; private set; } = true;
    [field: SerializeField] public AnimationCurve DeccelerationCurve { get; private set; } = null;

    [field: Header("Jumping")]
    [field: SerializeField] public bool JumpActive { get; private set; } = true;
    [field: SerializeField] public int MaxJumps { get; private set; } = 2;
    public bool CanJump => JumpsLeft > 0 && JumpActive;
    [field: SerializeField] public float JumpHeight { get; private set; } = 1f;
    [field: SerializeField] public float MinJumpTime { get; private set; } = 0.15f;
    [field: SerializeField] public AnimationCurve JumpCurve { get; private set; } = null;
    [field: SerializeField] public AnimationCurve FallCurve { get; private set; } = null; //Notiz: Bei Doppelsprung -> je länger man den ersten sprung abwartet desto höher der zweite
    [field: SerializeField] public float FallLerpSpeed { get; private set; } = 0.1f;

    [field: Header("Debugging")]
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;
    #endregion

    #region Character Variables
    [field: Header("Runtime Variables")]
    [field: SerializeField] public int JumpsLeft { get; set; } = 0;

    #endregion

    private void Awake()
    {
        Idle = new WolfIdle(this);
        Running = new WolfRunning(this);
        Jumping = new WolfJumping(this);
        Falling = new WolfFalling(this);
    }

    private void Start()
    {
        ChangeState(Idle);
    }

    protected override void Update()
    {
        base.Update();

        StateText.text = CurrentState.GetType().Name;
    }

    public override bool IsGrounded()
    {
        if (base.IsGrounded())
        {
            JumpsLeft = MaxJumps;
            return true;
        }

        return false;
    }
}
