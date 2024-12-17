using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementFleeAndSummon : EnemyMovementBase {
    public float summonDistance;
    public float fleeDistance;
    public float chaseDistance;

    private bool isChasing = true;
    private bool isFleeing = false;

    protected override IEnumerator MovementLogic() {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        Vector3 positionToMove = Vector3.zero;

        while (enabled && target != null) {
            if (agent != null && target != null) {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);

                if (isChasing) {
                    if (distanceToPlayer > summonDistance) {
                        // Chase
                        Debug.Log(target.position);
                       positionToMove = target.position;
                    } else {
                        // Attack
                        isChasing = false;
                        positionToMove = Vector3.zero;
                    }
                } else if (isAttacking) {
                    if (distanceToPlayer < fleeDistance) {
                        // Flee
                        isFleeing = true;
                        isAttacking = false;
                        positionToMove = CalculateFleePosition();
                    } else if (distanceToPlayer > chaseDistance) {
                        // Chase
                        isChasing = true;
                        isAttacking = false;
                    } else {
                        // Attack
                        positionToMove = Vector3.zero;
                    }
                } else if (isFleeing) {
                    if (distanceToPlayer > summonDistance) {
                        // Attack
                        isFleeing = false;
                        positionToMove = Vector3.zero;
                    } else {
                        // Flee
                        positionToMove = CalculateFleePosition();
                    }
                }

                if(positionToMove != Vector3.zero) {
                    agent.velocity = Vector3.zero;
                } else {
                    agent.SetDestination(positionToMove);
                }

                Debug.DrawLine(transform.position, positionToMove, Color.red, 0.5f);

                yield return wait;
            }
        }
    }

    private Vector3 CalculateFleePosition() {
        Vector3 directionAwayFromPlayer = (transform.position - target.position).normalized;
        Vector3 fleePosition = transform.position + directionAwayFromPlayer * fleeDistance;

        Vector3 fleeTarget;

        if (NavMesh.SamplePosition(fleePosition, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas)) {
            fleeTarget = hit.position;
        } else {
            fleeTarget = transform.position;
        }

        return fleeTarget;
    }
}
