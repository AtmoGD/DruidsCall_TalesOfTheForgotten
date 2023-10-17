using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public void ChangeState(State newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    protected virtual void Update()
    {
        CurrentState?.FrameUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState?.PhysicsUpdate();
    }
}
