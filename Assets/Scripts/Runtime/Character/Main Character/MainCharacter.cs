using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : GroundCharacter
{
    public MainState Idle { get; private set; }
    public MainState Running { get; private set; }
    public MainState Jumping { get; private set; }
    public MainState Falling { get; private set; }
    public MainState Landing { get; private set; }

#if UNITY_EDITOR
    [Header("Movement")]
    private string header = "Movement"; // Just a variable so i can use the header
#endif
    [field: SerializeField] public AnimationCurve AccelerationCurve { get; private set; } = null;
    [field: SerializeField] public AnimationCurve DecelerationCurve { get; private set; } = null;
    [field: SerializeField] public float SpeedMultiplier { get; private set; } = 1f;
    [field: SerializeField] public AnimationCurve FallCurve { get; private set; } = null;

    private void Awake()
    {
        Idle = new MainIdle(this);
        Running = new MainRunning(this);
        Jumping = new MainJumping(this);
        Falling = new MainFalling(this);
        Landing = new MainLanding(this);
    }

    private void Start()
    {
        ChangeState(Idle);
    }
}
