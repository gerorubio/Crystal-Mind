using UnityEngine;

[CreateAssetMenu(fileName = "Thorn Vines", menuName = "Artifacts/Effects/ThornVinesEffect")]
public class ThornVines : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase 1 HP
        player.CurrentHp += 1;
    }
}