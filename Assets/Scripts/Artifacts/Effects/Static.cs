using UnityEngine;

[CreateAssetMenu(fileName = "Static", menuName = "Artifacts/Effects/StaticEffect")]
public class Static : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase fire rate 10%
        player.CurrentFireRate += (player.CurrentFireRate * .1f);
    }

    public override void OnShoot(Character player, Weapon weapon, Dice dice, Projectile projectile) {
        // Every 10 attacks generate a lightning bolt that deals area damage.
        // TBI
    }
}