using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFollowAndAttack : EnemyMovementBase {
    public float attackDistance;
    public float chaseDistance;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private GameObject projectileSource;

    public float projectileForce = 10f;

    protected override IEnumerator MovementLogic() {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while (enabled && target != null) {
            if (agent != null && target != null) {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);

                RotateTowardsPlayer();

                if (isAttacking) { // Enemy is attacking
                    // Player is out of range
                    if (distanceToPlayer > chaseDistance) {
                        isAttacking = false;
                        animator.SetBool(isMoving, true);

                        // Ensure the agent speed is properly set during movement
                        agent.speed = speed;  // Ensure speed is set to a reasonable value, e.g., 1-3
                        agent.SetDestination(target.transform.position);  // Let agent move automatically
                    } else { // Player in range
                        agent.velocity = Vector3.zero;  // Stop movement during attack
                        animator.SetBool(attack, true); // Play attack animation
                    }
                } else { // Enemy is chasing the player
                    // In range for attack
                    if (distanceToPlayer < attackDistance) {
                        isAttacking = true;
                        agent.isStopped = true;  // Stop moving while attacking
                    } else { // Out of range
                        agent.isStopped = false;
                        animator.SetBool(isMoving, true);  // Play walking animation

                        // Set movement speed for chasing
                        agent.speed = speed;  // Ensure speed is reasonable for chasing (e.g., 1-3)
                        agent.SetDestination(target.transform.position);  // Continue chasing
                    }
                }

                yield return wait;
            }
        }
    }
}
