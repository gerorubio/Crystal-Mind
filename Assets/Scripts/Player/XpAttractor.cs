using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpAttractor : MonoBehaviour {
    private float radius;
    public float attractionSpeed;
    
    void Start() {
        Character player = GetComponent<Character>();
        if (player != null) {
            radius = player.CurrentXpCollectionRange;
        } else {
            Debug.LogWarning("No player found in XpAttractor");
        }
    }

    public void Update() {
        // Sphere around the player of radius = CurrentXpCollectionRange
        Collider[] elements = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider element in elements) {
            if(element.CompareTag("XP")) {
                MoveItemToPlayer(element.transform);
            }
        }
    }

    private void MoveItemToPlayer(Transform alceanistum) {
        Vector3 direction = (transform.position -  alceanistum.position).normalized;

        alceanistum.position = Vector3.MoveTowards(alceanistum.position, transform.position, attractionSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        // Visualize the attraction radius in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
