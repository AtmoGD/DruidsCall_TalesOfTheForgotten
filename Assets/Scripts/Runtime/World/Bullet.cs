using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField] public float MinLifeTime { get; set; } = 0.1f;
    [field: SerializeField] public float LifeTime { get; set; } = 1f;
    [field: SerializeField] public int DamageAmount { get; set; } = 1;
    [field: SerializeField] public float Speed { get; set; } = 1f;
    [field: SerializeField] public Vector2 KnockbackForce { get; set; } = Vector2.zero;
    [field: SerializeField] public GameObject DieEffect { get; set; } = null;
    [field: SerializeField] public LayerMask DieLayerMask { get; set; } = 0;
    private Vector2 direction = Vector2.zero;
    private float currentLifeTime = 0f;

    private void Update()
    {
        transform.Translate(direction * Speed * Time.deltaTime);

        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= LifeTime && currentLifeTime >= MinLifeTime)
            Die();
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void Die()
    {
        if (currentLifeTime < MinLifeTime) return;

        if (DieEffect)
            Instantiate(DieEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAttackable attackable = collision.GetComponent<IAttackable>();
        if (attackable != null)
        {
            attackable.TakeDamage(new Damage(this.gameObject, DamageAmount, KnockbackForce));
            Die();
        }

        if (DieLayerMask == (DieLayerMask | (1 << collision.gameObject.layer)))
            Die();
    }
}
