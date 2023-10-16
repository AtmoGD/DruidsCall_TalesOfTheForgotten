using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public virtual void Enter()
    {
        // Debug.Log("Entering State");
    }

    public virtual void FrameUpdate()
    {
        // Debug.Log("Updating State");

        DoStateChecks();
    }

    public virtual void PhysicsUpdate()
    {
        // Debug.Log("Updating Physics");
    }

    public virtual void DoStateChecks()
    {
        // Debug.Log("Doing State Checks");
    }

    public virtual void Exit()
    {
        // Debug.Log("Exiting State");
    }
}
