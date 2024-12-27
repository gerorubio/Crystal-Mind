using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LookCity : MonoBehaviour {
    public Transform targetLookPoint;
    public Transform targetMovePoint;
    public float lookSpeed = 2f;
    public float moveSpeed = 5f;
    public float stopDistance = 1f;

    private bool isTriggered = false;
    private bool uiActivated = false; // To prevent repeated activation
    private Transform player;
    private Camera mainCamera;
    private FirstPersonController firstPersonController;

    public GameObject remeberUI;
    public GameObject crt;

    private Volume volume;
    private Vignette vignette;

    // Intensity values
    private float vignetteIntensityOnTrigger = 1f;
    private float vignetteIntensityNormal = 0.27f;

    // Transition speed for vignette intensity
    public float vignetteTransitionSpeed = 5f;

    private void Start() {
        volume = crt.GetComponent<Volume>();

        // Get the Vignette
        if (volume.profile.TryGet(out vignette)) {
            vignette.intensity.value = vignetteIntensityNormal;
        } else {
            Debug.LogError("No Vignette effect found in the Volume profile!");
        }

        // Ensure the UI is inactive at the start
        if (remeberUI != null) {
            remeberUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.transform;
            mainCamera = Camera.main;
            firstPersonController = player.GetComponent<FirstPersonController>();

            if (firstPersonController != null) firstPersonController.enabled = false;

            isTriggered = true;
        }
    }

    private void Update() {
        if (isTriggered && player != null) {
            // Gradually increase vignette intensity
            if (vignette != null) {
                vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, vignetteIntensityOnTrigger, vignetteTransitionSpeed * Time.deltaTime);
                Debug.Log(vignette.intensity.value);
                // Check if the intensity reaches 0.95 and activate UI
                if (!uiActivated && vignette.intensity.value >= 0.75f) {
                    if (remeberUI != null) {
                        remeberUI.SetActive(true);
                        player.position = targetMovePoint.position;
                        uiActivated = true; // Prevent further activation
                    }

                    StartCoroutine(DisableUIAfterDelay(1.5f));
                }
            }

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
        } else {
            if (vignette != null) {
                vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, vignetteIntensityNormal, vignetteTransitionSpeed * Time.deltaTime);
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

    private IEnumerator DisableUIAfterDelay(float delay) {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Disable the UI after the delay
        if (remeberUI != null) {
            remeberUI.SetActive(false);
            uiActivated = false; // Reset the flag
        }
    }
}
