using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage
{
    public GameObject Attacker;
    public int DamageAmount;
    public Vector2 KnockbackForce;

    public Damage(GameObject attacker, int damageAmount, Vector2 knockbackForce)
    {
        Attacker = attacker;
        DamageAmount = damageAmount;
        KnockbackForce = knockbackForce;
    }

    public Damage Clone()
    {
        return new Damage(Attacker, DamageAmount, KnockbackForce);
    }
}

public interface IAttackable
{
    public UnityEvent<Damage> OnTakeDamage { get; set; }
    void TakeDamage(Damage damage);
}
