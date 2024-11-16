using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon {
    public GameObject bulletPrefab;

    protected override void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, transform);
    }
}
