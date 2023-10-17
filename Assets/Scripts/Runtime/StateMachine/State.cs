using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected float startTime;
    protected float timeInState => Time.time - startTime;


    public virtual void Enter()
    {
        startTime = Time.time;
    }

    public virtual void FrameUpdate()
    {
        DoStateChecks();
    }

    public virtual void PhysicsUpdate() { }

    public virtual void DoStateChecks() { }

    public virtual void Exit() { }
}
