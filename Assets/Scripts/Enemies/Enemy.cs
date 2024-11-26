using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public EnemySO enemySO;

    public float currentHp;

    public GameObject alceanistum;
    private GameObject alceanistumContainer;

    void Start() {
        if(enemySO == null) {
            Debug.LogError("Error: No EnemySO assigned to " + gameObject.name);
            return;
        }

        currentHp = enemySO.hp;

        alceanistumContainer = GameObject.Find("Alceanistum Container");

        if(alceanistumContainer == null) {
            Debug.Log("Error: No alceanistum container found");
        }
    }

    void Update() {
        
    }

    public void TakeDamage(float damage) {
        currentHp = Mathf.Max(currentHp - damage, 0);
        if(currentHp <= 0) {
            Die();
        }
    }

    private void Die() {
        ThrowXP();
        Destroy(gameObject);
    }

    private void ThrowXP() {
        if (alceanistum != null) {
            Instantiate(alceanistum, transform.position, Quaternion.Euler(90f, 0, 0));
        } else {
            Debug.LogWarning("Alceanistum prefab is not assigned.");
        }
    }
}