using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState : State
{
    protected Hero hero;

    public HeroState(Hero _hero)
    {
        this.hero = _hero;
    }
}
