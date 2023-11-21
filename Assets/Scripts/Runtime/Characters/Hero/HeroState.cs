using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState : CharacterState
{
    protected Hero hero;

    public HeroState(Hero _hero, string _animationName) : base(_hero, _animationName)
    {
        this.hero = _hero;
    }
}
