using UnityEngine;

[CreateAssetMenu(fileName = "Arsenic", menuName = "Artifacts/Effects/ArsenicEffect")]
public class Arsenic : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Poison Tick Time  reduced by 50%
        // TBI
        // Add poison effect to face
        face.effect = EffectType.Poison;
    }
}