using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(CooldownComponent))]
public class ObstacleComponent : MonoBehaviour
{
    [field: SerializeField] public bool IsSolid { get; private set; } = true;
    [field: SerializeField] public int Damage { get; private set; } = 1;
    [field: SerializeField] public float DamageCooldown { get; private set; } = 1f;
    [field: SerializeField] public Vector2 KnockbackForce { get; private set; } = new Vector2(1, 1);
    [field: SerializeField] public bool UseAbsoluteKnockback { get; private set; } = false;
    [field: SerializeField] public float GizmoLength { get; private set; } = 1f;

    private CooldownComponent cooldownComponent;

    private void Awake()
    {
        cooldownComponent = GetComponent<CooldownComponent>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HealthComponent healthComponent = collision.GetComponent<HealthComponent>();

        if (healthComponent != null && cooldownComponent.HasCooldown(healthComponent.gameObject.name) == false)
        {
            Vector2 knockbackForce;
            if (UseAbsoluteKnockback)
                knockbackForce = KnockbackForce;
            else
                knockbackForce = (transform.position - collision.transform.position).normalized * KnockbackForce;

            healthComponent.TakeDamage(new Damage(gameObject, Damage, knockbackForce));

            cooldownComponent.AddCooldown(new Cooldown(healthComponent.gameObject.name, DamageCooldown));
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        HealthComponent healthComponent = other.gameObject.GetComponent<HealthComponent>();

        if (healthComponent != null && cooldownComponent.HasCooldown(healthComponent.gameObject.name) == false)
        {
            Vector2 knockbackForce = (other.transform.position - transform.position).normalized * KnockbackForce;
            healthComponent.TakeDamage(new Damage(gameObject, Damage, knockbackForce));

            cooldownComponent.AddCooldown(new Cooldown(healthComponent.gameObject.name, DamageCooldown));
        }
    }

    private void OnDrawGizmos()
    {
        if (!UseAbsoluteKnockback) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - (Vector3)KnockbackForce * GizmoLength);
    }
}
