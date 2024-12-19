using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTornado : MonoBehaviour {
    private Transform target;

    public GameObject tornado;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Cast() {
        Instantiate(tornado, target.position, Quaternion.identity);
    }
}
