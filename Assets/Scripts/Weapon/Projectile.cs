using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float damage;
    public EffectType effect;
    public bool isPiercing;

    public bool splashDamage;

    public float force; // movement speed ???

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            // enemy take damage
        }
    }

}
