using UnityEngine;

[CreateAssetMenu(fileName = "Runners", menuName = "Artifacts/Effects/RunnersEffect")]
public class Runners : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase movement speed by 10%
        player.CurrentMovementSpeed += (player.CurrentMovementSpeed * .1f);
    }
}