using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementFleeAndSummon : EnemyMovementBase {
    public float summonDistance;
    public float fleeDistance;
    public float chaseDistance;

    private bool isChasing = true;

    protected override IEnumerator MovementLogic() {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while (enabled && target != null) {
            if (agent != null && target != null) {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);

                RotateTowardsPlayer();

                if (isChasing) { // Chasing logic
                    if (distanceToPlayer > summonDistance) {
                        agent.isStopped = false;
                        agent.speed = speed;
                        animator.SetBool(isMoving, true);
                        agent.SetDestination(target.position);
                    } else {
                        // Switch to attacking state
                        isChasing = false;
                        isAttacking = true;
                        agent.isStopped = true;
                        animator.SetBool(isMoving, false);
                        animator.SetBool(attack, true);
                    }
                } else if (isAttacking) { // Attacking logic
                    if (distanceToPlayer < fleeDistance) {
                        // Switch to fleeing state
                        isAttacking = false;
                        animator.SetBool(attack, false);
                        animator.SetBool(isMoving, true);

                        agent.isStopped = false;
                        agent.SetDestination(CalculateFleePosition());
                    } else if (distanceToPlayer > chaseDistance) {
                        // Resume chasing
                        isChasing = true;
                        isAttacking = false;

                        animator.SetBool(isMoving, true);
                        animator.SetBool(attack, false);
                    } else {
                        // Continue attack (stop agent movement)
                        agent.isStopped = true;
                    }
                } else { // Fleeing logic
                    if (distanceToPlayer > summonDistance) {
                        // Stop fleeing and prepare to attack
                        isAttacking = true;
                        agent.isStopped = true;
                        animator.SetBool(isMoving, false);
                        animator.SetBool(attack, true);
                    } else {
                        // Keep fleeing
                        agent.isStopped = false;
                        animator.SetBool(isMoving, true);
                        agent.SetDestination(CalculateFleePosition());
                    }
                }

                // Stop velocity explicitly when agent stops moving
                if (agent.isStopped) agent.velocity = Vector3.zero;

                Debug.DrawLine(transform.position, agent.destination, Color.red, 0.5f);
                yield return wait;
            }
        }
    }

    private Vector3 CalculateFleePosition() {
        Vector3 directionAwayFromPlayer = (transform.position - target.position).normalized;
        Vector3 fleePosition = transform.position + directionAwayFromPlayer * fleeDistance;

        if (NavMesh.SamplePosition(fleePosition, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas))
            return hit.position;

        return transform.position; // Fallback to current position
    }
}
