using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {
    private Transform target;
    public float updateSpeed = 0.1f; // How frequently to recalculate path

    private UnityEngine.AI.NavMeshAgent agent;

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Start () {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget() {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while(enabled) {
            if(agent != null) {
                agent.SetDestination(target.transform.position);

                yield return wait;
            }
        }
    }
}