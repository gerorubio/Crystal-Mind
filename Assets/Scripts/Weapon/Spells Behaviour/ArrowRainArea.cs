using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRainArea : MonoBehaviour {
    private SphereCollider sphereCollider;

    void Start() {
        sphereCollider = GetComponent<SphereCollider>();
        StartCoroutine(StartRain());
    }

    private IEnumerator StartRain() {
        yield return new WaitForSeconds(0.5f);

        sphereCollider.enabled = true;

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Arrow Rain collider");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy")) {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(20);
            }
        }
    }
}