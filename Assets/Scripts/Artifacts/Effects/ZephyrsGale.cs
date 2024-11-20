using UnityEngine;

[CreateAssetMenu(fileName = "Zephyrs Gale", menuName = "Artifacts/Effects/ZephyrsGaleEffect")]
public class ZephyrsGale : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase movement speed by 10%
        player.CurrentMovementSpeed += (player.CurrentMovementSpeed * .1f);
    }

    public override void OnSpellCast(Character player) {
        // Increases movement speed by 10% for 5 seconds after casting spell.
        // TBI
    }
}