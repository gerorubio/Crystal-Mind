using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public float speed;
    private Vector2 move;
    private Vector3 mousePositionViewport = Vector3.zero;
    private Quaternion desiredRotation = new Quaternion();

    private Vector3 targetDirection = Vector3.zero;
    public Vector3 TargetDirection {
        get { return targetDirection; }
        private set { targetDirection = value; }
    }

    public bool IsMoving => move.magnitude > 0.1f;


    void Update() {
        MovePlayer();
        RotatePlayer();
    }

    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }

    public void MovePlayer() {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement != Vector3.zero) {
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
            // Y position does not change
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

            desiredRotation = Quaternion.LookRotation(directionToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.1f);
        }
    }
}
