using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    private Vector2 move;
    private Vector3 targetDirection = Vector3.zero;
    private Character character; // Reference to the Character component

    public Vector3 TargetDirection {
        get { return targetDirection; }
        private set { targetDirection = value; }
    }

    // Keeps the IsMoving property to check if the player is moving
    public bool IsMoving => move.magnitude > 0.1f;

    void Start() {
        // Get the Character component attached to the player
        character = GetComponent<Character>();

        if (character == null) {
            Debug.LogError("Character component not found on the player.");
        }
    }

    void Update() {
        MovePlayer();
        RotatePlayer();
    }

    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }

    public void MovePlayer() {
        if (character == null) return; // Safety check

        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero) {
            // Use character's movement speed
            transform.Translate(movement * character.MovementSpeed * Time.deltaTime, Space.World);

            // Ensure Y position stays fixed
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            targetDirection = movement.normalized;
        }
    }

    public void RotatePlayer() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance)) {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 directionToMouse = (targetPoint - transform.position).normalized;

            // Only rotate on y
            directionToMouse.y = 0;

            Quaternion desiredRotation = Quaternion.LookRotation(directionToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.1f);
        }
    }
}
