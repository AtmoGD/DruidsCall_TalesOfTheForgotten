using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    public Enemy Enemy;

    public EnemyState(Enemy enemy)
    {
        this.Enemy = enemy;
    }
}
