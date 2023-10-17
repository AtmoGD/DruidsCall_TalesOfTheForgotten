using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : State
{
    protected MainCharacter character;

    public MainState(MainCharacter character)
    {
        this.character = character;
    }
}
