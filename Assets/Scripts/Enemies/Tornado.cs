using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Tornado : MonoBehaviour {
    public GameObject summonCircle;
    public GameObject tornado;

    void Start() {
        StartCoroutine(SummonTornado());
    }

    private IEnumerator SummonTornado() {
        summonCircle.SetActive(true);
        
        yield return new WaitForSeconds(1);

        //summonCircle.SetActive(false);

        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();

        capsuleCollider.enabled = true;

        tornado.SetActive(true);
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
