using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour {

    private Animator animator;
    private PlayerController playerController;

    void Start() {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update() {
        UpdateAnimation();
    }

    private void UpdateAnimation() {
        if (playerController == null || animator == null) return;

        if (playerController.IsMoving) {
            Vector3 directionToTarget = playerController.TargetDirection;

            float dot = Vector3.Dot(transform.forward, directionToTarget);

            animator.SetBool("isWalking", true);

            if (dot > 0.1f) { // Moving towards the mouse
                animator.SetBool("isWalkingForward", true);
            } else { // Moving away from the mouse
                animator.SetBool("isWalkingForward", false);
            }
        } else {
            // Idle animation
            animator.SetBool("isWalking", false);
        }
    }
}
