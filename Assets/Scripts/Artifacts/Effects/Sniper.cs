using UnityEngine;

[CreateAssetMenu(fileName = "Sniper", menuName = "Artifacts/Effects/SniperEffect")]
public class Sniper : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase attack range 10%
        player.CurrentAttackRange += (player.CurrentAttackRange * .2f);
    }
}