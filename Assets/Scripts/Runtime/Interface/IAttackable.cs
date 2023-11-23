using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public GameObject Attacker;
    public int DamageAmount;

    public Damage(GameObject attacker, int damageAmount)
    {
        Attacker = attacker;
        DamageAmount = damageAmount;
    }
}

public interface IAttackable
{
    void TakeDamage(Damage damage);
}
