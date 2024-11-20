using UnityEngine;

[CreateAssetMenu(fileName = "Madness", menuName = "Artifacts/Effects/MadnessEffect")]
public class Madness : ArtifactEffect {
    private int lastValue = 0;
    private float multiplier = 1f;

    public override void OnShoot(Character player, Weapon weapon, Dice dice, Projectile projectile) {
        // Every time you attack with a different value, multiply the damage by x1.1.
        // The multiplier increases by 0.1 for each different value.
        if (lastValue != dice.currentFace.value) {
            multiplier += 0.1f;
            projectile.Damage *= multiplier;
            lastValue = dice.currentFace.value;
        } else {
            multiplier = 1f;
        }
    }
}
