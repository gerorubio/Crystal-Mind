using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFleeAndSummon : EnemyMovementBase {
    public float summonDistance;
    public float fleeDistance;
    public float chaseDistance;

    private bool isChasing = true;
    private bool isFleeing =  false;

    protected override IEnumerator MovementLogic() {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while (enabled) {
            if (agent != null && target != null) {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);

                if (isChasing) {
                    if(distanceToPlayer > summonDistance) {
                        //Chase
                    } else {
                        // Attack
                    }
                } else if(isAttacking) {
                    if(distanceToPlayer < fleeDistance) {
                        // Flee
                    } else if(distanceToPlayer > chaseDistance) {
                        // Chase
                    } else {
                        // Attack
                    }
                } else if(isFleeing) {
                    if(distanceToPlayer > summonDistance) {
                        // Attack
                    } else {
                        // Flee
                    }
                }

                agent.SetDestination(target.transform.position);

                yield return wait;
            }
        }
    }

    protected override void AttackPlayer() {
        isAttacking = true;
        animator.SetBool(attack, true);
    }
}