using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolableObject {
    // Stats
    private float damage;
    private EffectType effect;
    private bool isPiercing;
    private float range;

    public bool splashDamage;
    public float force; // movement speed ???
    
    // Control
    private Vector3 endPosition;

    // GETTERS and SETTERS
    public float Damage { get => damage; set => damage = value; }
    public EffectType Effect { get => effect; set => effect = value; }
    public bool IsPiercing { get => isPiercing; set => isPiercing = value; }
    public float Force { get => force; set => force = value; }
    public float Range { get => range; set => range = value; }

    protected Rigidbody rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void OnDisable() {
        base.OnDisable();

        rigidBody.velocity = Vector3.zero;
    }

    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, endPosition) <= 0.1f) {
            Disable();
        }
    }

    public void InitProjectile(Weapon weapon, Face face) {
        damage = face.value;
        effect = face.effect;
        range = weapon.AttackRange;
        CalculateDestructionPoint();
    }

    private void CalculateDestructionPoint() {
        Vector3 startPosition = transform.position;
        Vector3 forwardDirection = transform.forward;
        endPosition = transform.position + transform.forward * range;
    }

    private void ApplyEffect(Enemy enemy) {
        switch(effect) {
            case EffectType.Burned:
                enemy.ApplyBurnedEffect();
                break;
            case EffectType.Frozen:
                enemy.ApplyFrozenEffect();
                break;
            case EffectType.Bleed:
                enemy.ApplyBleedEffect((int)Mathf.Floor(damage));
                break;
            case EffectType.Poison:
                enemy.ApplyPoisonEffect((int)Mathf.Floor(damage));
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null) {
                ApplyEffect(enemy);


                enemy.TakeDamage(damage);
            }

            if (!isPiercing) {
                Disable();
            }
        }
    }
}
