using UnityEngine;

[CreateAssetMenu(fileName = "Gunpowder Boost", menuName = "Artifacts/Effects/GunpowderBoostEffect")]
public class GunpowderBoost : ArtifactEffect {
    public override void OnShoot(Character player, Weapon weapon, Dice dice, Projectile projectile) {
        // A die doubles its damage if its rolled value is at least half of its maximum
        if (dice.currentFace.value >= dice.Faces.Length / 2) {
            projectile.Damage = projectile.Damage * 2;
        }
    }
}