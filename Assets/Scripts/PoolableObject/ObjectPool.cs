using System.Collections.Generic;
using UnityEngine;

public class ObjectPool {
    private PoolableObject prefab;
    private int size;
    private List<PoolableObject> avaliableObjects;

    private ObjectPool(PoolableObject prefab, int size) {
        this.prefab = prefab;
        this.size = size;
        avaliableObjects = new List<PoolableObject>(size);
    }

    public static ObjectPool CreateInstance(PoolableObject prefab, int size) {
        ObjectPool pool = new ObjectPool(prefab, size);

        GameObject poolObject = new GameObject(prefab.name + " Pool");
        pool.CreateObjects(poolObject.transform);

        return pool;
    }

    private void CreateObjects(Transform parent) {
        for (int i = 0; i < size; i++) {
            PoolableObject poolableObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
            poolableObject.parent = this;
            poolableObject.gameObject.SetActive(false);
        }
    }

    public PoolableObject GetObject() {
        if(avaliableObjects.Count > 0) {
            PoolableObject instance = avaliableObjects[0];
            avaliableObjects.RemoveAt(0);

            instance.gameObject.SetActive(true);

            return instance;
        } else {
            PoolableObject poolableObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            poolableObject.parent = this;

            return poolableObject;
        }
    }

    public void ReturnObjectToPool(PoolableObject poolableObject) {
        avaliableObjects.Add(poolableObject);
    }
}
