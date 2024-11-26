using UnityEngine;

public class CursorFollower : MonoBehaviour {
    public Texture2D aim;
    public float zOffset;

    private Vector3 targetPosition;
    private bool autoAim = false; // Flag to toggle behavior.

    private Camera mainCamera; // Reference to the main camera.

    PlayerController playerController;

    void Start() {
        // Assign the main camera if not set.
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }

        // Set cursor appearance
        if (aim != null) {
            Vector2 hotspot = new Vector2(aim.width, aim.height);
            Cursor.SetCursor(aim, hotspot, CursorMode.ForceSoftware);
        }

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (playerController != null) {
            playerController.OnAutoAim += AutoAim;
        } else {
            Debug.LogError("PlayerController component not found!");
        }
    }

    void Update() {
        if (autoAim) {

            Vector3 position = playerController.GetClosestEnemy();
            targetPosition = new Vector3 (position.x, 4f, position.z - zOffset);
            // Move towards the target position.
            transform.position = targetPosition;
        } else {
            FollowCursor();
        }
    }

    private void AutoAim() {
        autoAim = !autoAim;
    }

    void FollowCursor() {
        // Get the mouse position in screen space and convert to world space.
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z); // Maintain depth.
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Instantly set the position to the cursor.
        transform.position = worldPosition;
    }
}