using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon {
    public GameObject bulletPrefab;

    protected override void Shoot() {
        if (isGamePaused || isReloading) {
            return;
        }

        if(Time.time < nextFiretime) {
            return;
        }

        if (ammunitionSystem.CurrentAmmunition.Count > 0) {
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

            Projectile projectile = bullet.AddComponent<Projectile>();
            CreateProjectile(projectile, face);
            
            float force = projectile.force;

            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * force, ForceMode.Impulse);


            nextFiretime = Time.time + fireRate;

        } else if(!isReloading) {
            StartCoroutine(ReloadRoutine());
        }

        UpdateDisplay();
    }

}
