using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
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

    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, endPosition) <= 0.1f) {
            Destroy(gameObject);
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

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            // enemy take damage
        }
    }
}
