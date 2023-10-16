using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : GroundCharacter
{
    public MainState Idle { get; private set; }
    public MainState Accelerating { get; private set; }
    public MainState Moving { get; private set; }
    public MainState Running { get; private set; }
    public MainState Deccelerating { get; private set; }
    public MainState Jumping { get; private set; }
    public MainState Falling { get; private set; }
    public MainState Landing { get; private set; }

#if UNITY_EDITOR
    [Header("Movement")] private string movementHeader; // Just a variable so i can use the header
#endif
    [field: SerializeField] public AnimationCurve AccelerationCurve { get; private set; } = null;
    [field: SerializeField] public bool ResetAccelerationOnDirectionChange { get; private set; } = true;
    [field: SerializeField] public AnimationCurve DeccelerationCurve { get; private set; } = null;
    [field: SerializeField] public float MaxSpeed { get; private set; } = 1f;
    [field: SerializeField] public AnimationCurve FallCurve { get; private set; } = null;

#if UNITY_EDITOR
    [Header("Debugging")] private string debuggingHeader; // Just a variable so i can use the header
#endif
    [field: SerializeField] public TMPro.TMP_Text StateText { get; private set; } = null;

    private void Awake()
    {
        Idle = new MainIdle(this);
        Moving = new MainMoving(this);
        Accelerating = new MainAccelerating(this);
        Running = new MainRunning(this);
        Deccelerating = new MainDeccelerating(this);
        Jumping = new MainJumping(this);
        Falling = new MainFalling(this);
        Landing = new MainLanding(this);
    }

    private void Start()
    {
        ChangeState(Idle);
    }
}
