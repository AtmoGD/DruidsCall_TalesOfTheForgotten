using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : State
{
    protected Character character;
    protected float startTime;
    protected float timeInState => Time.time - startTime;

    public CharacterState(Character character)
    {
        this.character = character;

        startTime = Time.time;
    }
}
