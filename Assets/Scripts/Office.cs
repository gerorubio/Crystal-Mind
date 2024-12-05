using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour {
    public float speed;
    public Vector3 endPosition;

    public float delay;

    private bool isMoving = false;

    private void Start() {
        StartCoroutine(Delay());
    }

    void Update() {
        if (isMoving) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, endPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, endPosition) <= 1f) {
                transform.localPosition = endPosition;
                enabled = false;
            }
        }
    }

    private IEnumerator Delay() {
        yield return new WaitForSeconds(delay);

        isMoving = true;
    }
}