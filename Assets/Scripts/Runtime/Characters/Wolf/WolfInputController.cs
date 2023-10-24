using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfGoal
{
    Idle,
    Move,
    Jump,
    Attack,
    Die
}

public class WolfInputController : MonoBehaviour
{
    public WolfInput WolfInput { get; protected set; } = new WolfInput();
    public WolfGoal CurrentGoal { get; protected set; } = WolfGoal.Idle;

    private void Update()
    {
        DoGoalChecks();
    }

    private void DoGoalChecks()
    {

    }
}
