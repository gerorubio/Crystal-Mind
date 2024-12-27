using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LookPropaganda : MonoBehaviour {
    public Transform targetLookPoint;
    public Transform targetMovePoint;
    public float lookSpeed = 2f;
    public float moveSpeed = 5f;
    public float stopDistance = 1f;

    private bool isTriggered = false;
    private Transform player;
    private Camera mainCamera;
    private FirstPersonController firstPersonController;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.transform;
            mainCamera = Camera.main;
            firstPersonController = player.GetComponent<FirstPersonController>();

            if (firstPersonController != null) firstPersonController.enabled = false;

            isTriggered = true;

            StartCoroutine(GoToMainMenuAfterDelay(4f));
        }
    }

    private void Update() {
        if (isTriggered && player != null) {

            // Rotate the camera to look at the target
            Quaternion targetLookRotation = Quaternion.LookRotation(targetLookPoint.position - mainCamera.transform.position);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetLookRotation, lookSpeed * Time.deltaTime);

            // Move the player towards the target point
            if (targetMovePoint != null) {
                Vector3 moveDirection = (targetMovePoint.position - player.position).normalized;
                player.position += moveDirection * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(player.position, targetMovePoint.position) <= stopDistance) {
                    StopForcingActions(targetLookRotation);
                }
            }
        }
    }

    private void StopForcingActions(Quaternion finalRotation) {
        isTriggered = false;

        // Align the player's rotation with the camera's final rotation
        if (player != null) {
            player.rotation = Quaternion.Euler(0, finalRotation.eulerAngles.y, 0);
        }

        if (firstPersonController != null) {
            // Re-enable the first-person controller
            firstPersonController.enabled = true;
        }
    }
    private IEnumerator GoToMainMenuAfterDelay(float delay) {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);
        firstPersonController.lockCursor = false;
        // Change scene after the delay
        SceneManager.LoadScene("StartMenu"); // Change the scene to "StartMenu"
    }

}
