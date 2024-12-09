using UnityEngine;

public class DroneProjectile : MonoBehaviour {
    public float speed = 1f;

    private void Update() {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        // Add collision behavior (e.g., destroy on impact)
        Destroy(gameObject);
    }
}
