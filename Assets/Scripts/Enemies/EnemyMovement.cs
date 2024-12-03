using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {
    private Transform target;
    public float updateSpeed = 0.1f; // How frequently to recalculate path

    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;
    // States
    private const string isMoving = "IsMoving";
    private const string attack = "Attack";

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start () {
        animator.SetBool(isMoving, agent.velocity.magnitude > 0.01f);
        StartCoroutine(FollowTarget());
    }

    private void Update() {
        if (animator != null) {
            animator.SetBool(isMoving, agent.velocity.magnitude > 0.01f);
        }
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