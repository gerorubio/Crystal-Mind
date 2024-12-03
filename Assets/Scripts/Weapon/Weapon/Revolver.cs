using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon {
    public GameObject bulletPrefab;

    // Bullet pool
    private ObjectPool bulletPool;
    public Projectile projectile;

    protected override void Awake() {
        base.Awake();
        bulletPool = ObjectPool.CreateInstance(projectile, 50);
    }

    protected override void Shoot() {
        // Get object from pool
        PoolableObject instance = bulletPool.GetObject();

        if(instance != null) {

            // Roll dice and get the result face
            Dice dice = ammunitionSystem.CurrentAmmunition[0];
            Face face = dice.currentFace;
            // Remove dice from ammo
            ammunitionSystem.CurrentAmmunition.RemoveAt(0);

            // Get the Z-axis rotation from the Player GameObject
            float playerYRotation = transform.parent.eulerAngles.y;
            Quaternion zRotation = Quaternion.Euler(0f, playerYRotation, 0f);

            // Position
            instance.transform.position = projectileSource.transform.position;
            instance.transform.rotation = zRotation;

            // Set parent
            instance.transform.SetParent(parentProjectiles.transform);
            // Get Projectile Component
            Projectile projectile = instance.GetComponent<Projectile>();
            // Initialize projectile
            projectile.InitProjectile(this, face);

            // Called event so OnShoot artifacts trigger
            TriggerOnShoot(dice, projectile);
            
            float force = projectile.Force;

            instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * force, ForceMode.Impulse);

            audioSource.Play();
        }
    }

}
