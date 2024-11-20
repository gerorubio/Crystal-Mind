using UnityEngine;

[CreateAssetMenu(fileName = "Seeing Double", menuName = "Artifacts/Effects/SeeingDoubleEffect")]
public class SeeingDouble: ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase fire rate 10%
        player.CurrentFireRate += (player.CurrentFireRate * .1f);
    }

    public override void OnShoot(Character player, Weapon weapon, Dice dice, Projectile projectile) {
        // Each time you shoot a die, there is a 10% chance of obtaining an additional die for one use.
        if (Random.Range(0, 100) < 10) {
            weapon.AmmunitionSystem.CurrentAmmunition.Add(dice);
        }
    }
}