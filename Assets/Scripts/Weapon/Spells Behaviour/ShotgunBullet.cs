using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour {
    public float speed = 10f; // Speed of the bullet
    public float lifetime = 2f; // Time before the bullet is destroyed

    private Rigidbody rb;

    private void Awake() {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        // Apply initial velocity to the Rigidbody
        rb.velocity = transform.forward * speed;

        // Schedule the bullet to be destroyed after the lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(20);
            }
        }
    }
}
