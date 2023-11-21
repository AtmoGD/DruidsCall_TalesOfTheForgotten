using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : State
{
    protected Character character;

    public CharacterState(Character _character)
    {
        this.character = _character;
    }
}
