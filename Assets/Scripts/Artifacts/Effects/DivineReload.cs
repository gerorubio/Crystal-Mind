using UnityEngine;

[CreateAssetMenu(fileName = "Divine Reload", menuName = "Artifacts/Effects/DivineReloadEffect")]
public class DivineReload : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase reload speed by 10%.
        player.CurrentReloadSpeed += (player.CurrentReloadSpeed * .1f);
    }

    public override void OnReload(Character player, Weapon weapon) {
        // Gain a 10 % chance for an instant reload.
        // TBI
    }
}
