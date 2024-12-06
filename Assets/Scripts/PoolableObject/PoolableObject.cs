using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolableObject : MonoBehaviour {
    public ObjectPool parent;

    private const string DisableMethodName = "Disable";

    public virtual void OnEnable() {
        CancelInvoke(DisableMethodName);
    }

    public virtual void OnDisable() {
        parent.ReturnObjectToPool(this);
    }

    public virtual void Disable() {
        gameObject.SetActive(false);
    }
}