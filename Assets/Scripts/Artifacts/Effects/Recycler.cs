using UnityEngine;

[CreateAssetMenu(fileName = "Recycler", menuName = "Artifacts/Effects/RecyclerEffect")]
public class Recycler : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase reload speed by 10%.
        player.CurrentReloadSpeed += (player.CurrentReloadSpeed * .1f);
    }

    public override void OnReload(Character player, Weapon weapon) {
        // When reloading, for each unused die, the reload speed increases by 2%.
        // TBI
    }
}