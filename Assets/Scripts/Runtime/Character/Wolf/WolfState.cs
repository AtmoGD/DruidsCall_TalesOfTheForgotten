using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfState : State
{
    protected Wolf wolf;

    public WolfState(Wolf _wolf)
    {
        this.wolf = _wolf;
    }
}
