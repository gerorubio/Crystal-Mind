using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Spells/Effects/ShotgunEffect")]
public class Shotgun : SpellEffect {
    public GameObject projectile;
    public float spreadAngle = 45f;
    public float radius = 1f;

    public override void Cast(Character player) {
        Vector3 origin = player.transform.position;
        Vector3 forward = player.transform.forward;

        for (int i = 0; i < 8; i++) {
            float angle = i * spreadAngle;

            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * forward;

            // Instantiate projectile
            GameObject spawnedProjectile = Instantiate(projectile, origin + direction * radius, Quaternion.LookRotation(direction));
            spawnedProjectile.transform.forward = direction;
        }
    }
}
