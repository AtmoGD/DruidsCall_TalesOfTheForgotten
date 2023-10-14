using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : State
{
    protected MainCharacter character;
    protected float startTime;
    protected float timeInState => Time.time - startTime;

    public MainState(MainCharacter character)
    {
        this.character = character;

        startTime = Time.time;
    }
}
