using UnityEngine;

[CreateAssetMenu(fileName = "Snake Nest", menuName = "Artifacts/Effects/SnakeNestEffect")]
public class SnakeNest : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Change value of a face to 2
        face.value = 2;
    }

    public override void OnShoot(Character player, Weapon weapon, Dice dice, Projectile projectile) {
        // All dices with a value of 2 do a critical hit.
        if (dice.currentFace.value == 2) {
            projectile.Damage *= 3;
        }
    }
}