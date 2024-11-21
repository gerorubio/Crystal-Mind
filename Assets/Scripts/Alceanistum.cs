using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alceanistum : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player")) {
            Character player =  collision.gameObject.GetComponent<Character>();
            player.GainXP(1);
            Destroy(gameObject);
        }
    }
}
