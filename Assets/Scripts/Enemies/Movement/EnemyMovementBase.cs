using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyMovementBase : MonoBehaviour {
    // Player
    protected Transform target;
    // How frequently to recalculate path
    public float updateSpeed = 0.1f;
    // Nav Mesh Agent
    protected NavMeshAgent agent;
    // Walk speed
    protected float speed;

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
        for(int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).gameObject.tag == "Enemy") {
                speed = transform.GetChild(i).GetComponent<Enemy>().enemySO.speed;
            }
        }

        StartCoroutine(MovementLogic());
    }

    private void Update() {
        if (animator != null) {
            animator.SetBool(isMoving, agent.velocity.magnitude > 0.01f);
        }
    }

    protected void RotateTowardsPlayer() {
        if (target == null) return;

        // Calculate the direction to the player
        Vector3 directionToPlayer = (target.position - transform.position).normalized;

        // Rotate the parent object smoothly (keep y-axis rotation only)
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Smooth rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 500f);
    }

    protected abstract IEnumerator MovementLogic();
}
