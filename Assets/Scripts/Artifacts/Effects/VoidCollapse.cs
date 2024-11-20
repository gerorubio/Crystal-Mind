using UnityEngine;

[CreateAssetMenu(fileName = "Void Collapse", menuName = "Artifacts/Effects/VoidCollapseEffect")]
public class VoidCollapse: ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Change value of a face to 1
        face.value = 1;
    }

    public override void OnShoot(Character player, Weapon weapon, Dice dice, Projectile projectile) {
        // Each die with a value of 1 causes an explosion that damages nearby enemies.
        if (dice.currentFace.value == 1) {
            // TBI
        }
    }
}