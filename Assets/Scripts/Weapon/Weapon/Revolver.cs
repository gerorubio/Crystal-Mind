using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon {
    public GameObject bulletPrefab;

    protected override void Shoot() {
        // Roll dice and get the result face
        Face face = ammunitionSystem.CurrentAmmunition[0].currentFace;
        // Remove dice from ammo
        ammunitionSystem.CurrentAmmunition.RemoveAt(0);

        // Get the Z-axis rotation from the Player GameObject
        float playerYRotation = transform.parent.eulerAngles.y;
        Quaternion zRotation = Quaternion.Euler(0f, playerYRotation, 0f);

        // Instantiate the bullet
        GameObject bullet = Instantiate(
            bulletPrefab,
            projectileSource.transform.position,
            zRotation
        );
        // Set parent
        bullet.transform.SetParent(parentProjectiles.transform);
        // Get Projectile Component
        Projectile projectile = bullet.GetComponent<Projectile>();
        // Initialize projectile
        projectile.InitProjectile(this, face);
            
        float force = projectile.Force;

        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * force, ForceMode.Impulse);
    }

}