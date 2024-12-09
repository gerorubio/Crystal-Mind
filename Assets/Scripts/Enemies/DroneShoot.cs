using System.Collections;
using UnityEngine;

public class DroneShoot : MonoBehaviour {
    // Public variables to assign in the Inspector
    public GameObject projectileSource; // The origin point of the projectile
    public GameObject projectilePrefab; // The projectile to be instantiated

    // Shooting interval
    public float shootInterval = 2f;

    private void Start() {
        // Start the shooting loop
        StartCoroutine(ShootProjectile());
    }

    private IEnumerator ShootProjectile() {
        WaitForSeconds wait = new WaitForSeconds(shootInterval);

        while (enabled) {
            // Ensure both source and prefab are set
            if (projectileSource != null && projectilePrefab != null) {
                // Instantiate the projectile at the source's position and rotation
                Instantiate(projectilePrefab, projectileSource.transform.position, projectileSource.transform.rotation);
            } else {
                Debug.LogWarning("ProjectileSource or ProjectilePrefab is not set on " + gameObject.name);
            }

            // Wait for the specified interval before shooting again
            yield return wait;
        }
    }
}
