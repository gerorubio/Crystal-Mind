using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Whetstone", menuName = "Artifacts/Effects/WhetstoneEffect")]
public class Whetstone : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Change value of a face to 2
        face.value = 2;
    }
}