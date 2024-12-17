using System.Collections;
using UnityEngine;

public class DroneShoot : MonoBehaviour {
    public GameObject projectileSource;
    public GameObject projectilePrefab;

    public float projectileForce;

    public void ShootProjectile() {
        // Ensure both source and prefab are set
        if (projectileSource != null && projectilePrefab != null) {
            // Instantiate the projectile at the source's position and rotation
            GameObject newProjectile = Instantiate(projectilePrefab, projectileSource.transform.position, Quaternion.identity);

            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            if (rb != null) {
                // Apply force in the forward direction of the projectile source
                rb.AddForce(projectileSource.transform.forward * projectileForce, ForceMode.Impulse);
            } else {
                Debug.LogWarning("ProjectilePrefab is missing a Rigidbody!");
            }
        } else {
            Debug.LogWarning("ProjectileSource or ProjectilePrefab is not set on " + gameObject.name);
        }
    }
}
