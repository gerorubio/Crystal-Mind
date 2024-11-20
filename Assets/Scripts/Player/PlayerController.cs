using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour {
    // Input system
    private PlayerInput playerInput;
    // Store our controls
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction reloadAction;
    private InputAction aimAction;
    private InputAction dashAction;
    private InputAction spellCastAction;

    private Vector2 move;
    private Vector3 targetDirection = Vector3.zero;
    private Character character;
    private bool autoAim = false;

    public Vector3 TargetDirection {
        get { return targetDirection; }
        private set { targetDirection = value; }
    }

    // Events
    public event Action<Vector3> OnAutoAim;
    public event Action OnShoot;
    public event Action OnReload;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        shootAction = playerInput.actions["Shoot"];
        reloadAction = playerInput.actions["Reload"];
        aimAction = playerInput.actions["Aim"];
        dashAction = playerInput.actions["Dash"];
        spellCastAction = playerInput.actions["Cast Spell"];
    }

    void Start() {
        character = GetComponent<Character>();

        if (character == null) {
            Debug.LogError("Character component not found on the player.");
        }
    }

    void Update() {
        if (GameManager.GameIsPaused) return;

        move = moveAction.ReadValue<Vector2>();
        MovePlayer();

        if (aimAction.triggered) {
            autoAim = !autoAim;
        }

        if(shootAction.ReadValue<float>() > 0) {
            OnShoot?.Invoke();
        }

        if (reloadAction.ReadValue<float>() > 0) {
            OnReload?.Invoke();
        }

        RotatePlayer();
    }

    // Check if the player is moving
    public bool IsMoving => move.magnitude > 0.1f;

    public void MovePlayer() {
        if (character == null) return; // Safety check

        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero) {
            // Use character's movement speed
            transform.Translate(movement * character.MovementSpeed * Time.deltaTime, Space.World);

            // Constraint in y axis
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            targetDirection = movement.normalized;
        }
    }

    public void RotatePlayer() {
        Vector3 targetPoint;

        if (autoAim) {
            targetPoint = GetClosestEnemy();
            OnAutoAim?.Invoke(Camera.main.WorldToScreenPoint(targetPoint));
        } else {
            targetPoint = GetMousePosition();
        }

        Vector3 directionToMouse = (targetPoint - transform.position).normalized;

        // Only rotate on y
        directionToMouse.y = 0;

        Quaternion desiredRotation = Quaternion.LookRotation(directionToMouse);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.9f);
    }

    private Vector3 GetMousePosition() {
        Vector3 targetPosition = Vector3.zero;
        // Create a ray from the camnera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Create plane xz and y = 0
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float distance)) {
            targetPosition = ray.GetPoint(distance);
        }

        return targetPosition;

    }

    private Vector3 GetClosestEnemy() {
        Vector3 targetPosition = Vector3.zero;
        Vector3 currentPosition = transform.position;

        Vector3 bestPosition;

        // Set the initial closest distance to infinity (to ensure any valid distance will be smaller)
        float closestDistanceSqr = Mathf.Infinity;
        // Find all game objects tagged as "Enemy" in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0) {
            foreach (GameObject enemy in enemies) {
                // Calculate the direction vector from the current object to the enemy
                Vector3 directionToTarget = enemy.transform.position - currentPosition;

                // Calculate the squared distance to the enemy (no need for square root for comparison)
                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr) {
                    closestDistanceSqr = dSqrToTarget;
                    bestPosition = enemy.transform.position;
                    targetPosition = bestPosition;
                }
            }

        } else {
            targetPosition = GetMousePosition();
        }

        return targetPosition;
    }
}
