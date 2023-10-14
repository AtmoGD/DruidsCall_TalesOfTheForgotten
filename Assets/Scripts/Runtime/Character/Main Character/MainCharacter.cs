using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : GroundCharacter
{
    public MainState Idle { get; private set; }
    public MainState Falling { get; private set; }
    public MainState Landing { get; private set; }


    [field: SerializeField] public AnimationCurve FallCurve { get; private set; } = null;

    private void Awake()
    {
        Idle = new MainIdle(this);
        Falling = new MainFalling(this);
        Landing = new MainLanding(this);
    }

    private void Start()
    {
        ChangeState(Idle);
    }

    protected new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
