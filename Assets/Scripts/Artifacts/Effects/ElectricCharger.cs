using UnityEngine;

[CreateAssetMenu(fileName = "Electric Charger", menuName = "Artifacts/Effects/ElectricChargerEffect")]
public class ElectricCharger : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase reload speed by 10%.
        player.CurrentReloadSpeed += (player.CurrentReloadSpeed * .1f);
    }

    public override void OnReload(Character player, Weapon weapon) {
        // Deal 20 damage to all enemies when you reload.
        // TBI
    }
}