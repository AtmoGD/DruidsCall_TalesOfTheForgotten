using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState : CharacterState
{
    protected Hero hero;

    public HeroState(Hero _hero) : base(_hero)
    {
        this.hero = _hero;
    }
}
