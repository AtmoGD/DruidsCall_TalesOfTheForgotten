using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : State
{
    protected Character character;

    public CharacterState(Character _character, string _animationName)
    {
        this.character = _character;

        if (character.Animator != null)
            character.Animator.SetTrigger(_animationName);
    }
}
