using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitTrigger : Trigger, IAttackable
{
    public UnityEvent<Damage> OnTakeDamage { get; set; } = new UnityEvent<Damage>();

    public void TakeDamage(Damage damage)
    {
        TriggerEvent();
    }
}
