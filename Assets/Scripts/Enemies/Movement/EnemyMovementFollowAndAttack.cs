using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFollowAndAttack : EnemyMovementBase {
    public float attackDistance;
    public float chaseDistance;

    protected override IEnumerator MovementLogic() {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while (enabled) {
            if (agent != null && target != null) {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);

                if(isAttacking) { // Enemy is attacking
                    // Player is out of range
                    if(distanceToPlayer > chaseDistance) {
                        isAttacking = false;
                        agent.isStopped = false;
                        animator.SetBool(attack, false);
                    } else { // Player in range
                        agent.isStopped = true;
                        AttackPlayer();
                    } 
                } else { // enemy is chasing the player
                    // In range
                    if(distanceToPlayer < attackDistance) {
                        isAttacking = true;
                        agent.isStopped = true;
                    } else { // out of range
                        agent.isStopped = false;
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