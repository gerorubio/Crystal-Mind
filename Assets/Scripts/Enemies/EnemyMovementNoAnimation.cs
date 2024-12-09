using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementNoAnimation : MonoBehaviour {
    private Transform target;
    public float updateSpeed = 0.1f; // How frequently to recalculate path

    private NavMeshAgent agent;

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget() {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while (enabled) {
            if (agent != null && target != null) {
                agent.SetDestination(target.position);
            }
            yield return wait;
        }
    }
}