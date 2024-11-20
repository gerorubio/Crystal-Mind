using UnityEngine;

[CreateAssetMenu(fileName = "Guillotine", menuName = "Artifacts/Effects/GuillotineEffect")]
public class Guillotine : ArtifactEffect {

    public override void OnShoot(Character player, Weapon weapon, Dice dice, Projectile projectile) {
        // 1% chance to deal 999 damage.
        if (Random.Range(0, 100) == 1) {
            projectile.Damage = 999;
        }
    }
}