using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementFollow : EnemyMovementBase {
    protected override IEnumerator MovementLogic() {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while(enabled && target != null) {
            if(agent != null && target != null) {
                agent.SetDestination(target.transform.position);

                yield return wait;
            }
        }
    }
}