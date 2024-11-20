using UnityEngine;

[CreateAssetMenu(fileName = "Cupids Arrow", menuName = "Artifacts/Effects/CupidsArrowEffect")]
public class CupidsArrow : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Grant piercing
        weapon.Piercing = true;
        // Add obsidian effect to face
        face.effect = EffectType.Obsidian;
    }
}