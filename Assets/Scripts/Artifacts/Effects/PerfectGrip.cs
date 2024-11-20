using UnityEngine;

[CreateAssetMenu(fileName = "Perfect Grip", menuName = "Artifacts/Effects/PerfectGripEffect")]
public class PerfectGrip : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase fire rate 10%
        player.CurrentFireRate += (player.CurrentFireRate * .1f);
    }

    public override void OnReload(Character player, Weapon weapon) {
        // Increases attack speed by 20% for each die with an even value.
        // TBI
    }
}