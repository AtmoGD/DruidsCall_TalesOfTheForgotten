using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfState : CharacterState
{
    protected Wolf wolf;

    public WolfState(Wolf _wolf) : base(_wolf)
    {
        this.wolf = _wolf;
    }
}
