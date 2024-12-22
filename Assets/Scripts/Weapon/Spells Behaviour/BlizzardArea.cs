using System.Collections;
using UnityEngine;

public class BlizzardArea : MonoBehaviour {
    private SphereCollider sphereCollider;

    void Start() {
        sphereCollider = GetComponent<SphereCollider>();

        if (sphereCollider == null) {
            Debug.LogError("SphereCollider component is missing on " + gameObject.name);
            return;
        }

        StartCoroutine(StartStorm());
    }

    private IEnumerator StartStorm() {
        float toggleInterval = 0.5f;
        int toggleCount = 5;

        for (int i = 0; i < toggleCount; i++) {
            sphereCollider.enabled = !sphereCollider.enabled;
            yield return new WaitForSeconds(toggleInterval);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other == null) return;

        Debug.Log($"BlizzardArea triggered by: {other.gameObject.name}");

        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.ApplyFrozenEffect();
                Debug.Log($"{other.gameObject.name} frozen by BlizzardArea.");
            } else {
                Debug.LogWarning($"Enemy tag detected but no Enemy component found on {other.gameObject.name}.");
            }
        }
    }
}
