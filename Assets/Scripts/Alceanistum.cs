using UnityEngine;

public class Alceanistum : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Character player = other.gameObject.GetComponent<Character>();
            player.GainXP(1);
            Destroy(gameObject);
        }
    }
}
