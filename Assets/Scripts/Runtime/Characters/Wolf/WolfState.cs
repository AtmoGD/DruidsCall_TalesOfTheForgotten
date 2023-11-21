using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfState : CharacterState
{
    protected Wolf wolf;

    public WolfState(Wolf _wolf, string _animationName) : base(_wolf, _animationName)
    {
        this.wolf = _wolf;
    }
}
