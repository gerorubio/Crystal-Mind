using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyMovementBase : MonoBehaviour {
    // Player
    protected Transform target;
    // How frequently to recalculate path
    public float updateSpeed = 0.1f;

    protected NavMeshAgent agent;

    [SerializeField]
    protected Animator animator;
    // States
    protected const string isMoving = "IsMoving";
    protected const string attack = "Attack";

    protected bool isAttacking = false;

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        StartCoroutine(MovementLogic());
    }

    private void Update() {
        if (animator != null) {
            animator.SetBool(isMoving, agent.velocity.magnitude > 0.01f);
        }
    }

    protected abstract IEnumerator MovementLogic();
    protected abstract void AttackPlayer();
}
