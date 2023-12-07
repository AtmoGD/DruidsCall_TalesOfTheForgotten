using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent<Damage> OnTakeDamage { get; set; }
    void TakeDamage(Damage damage);
}
