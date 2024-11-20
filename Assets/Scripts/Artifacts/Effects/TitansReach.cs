using UnityEngine;

[CreateAssetMenu(fileName = "Titans Reach", menuName = "Artifacts/Effects/TitansReachEffect")]
public class TitansReach : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase projectile size 10%
        weapon.ProjectileSize += (weapon.ProjectileSize * .1f);
        // Increase knockback 10%
        weapon.Knockback += (weapon.Knockback * .1f);
    }
}