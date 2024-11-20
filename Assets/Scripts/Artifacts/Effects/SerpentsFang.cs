using UnityEngine;

[CreateAssetMenu(fileName = "Serpents Fang", menuName = "Artifacts/Effects/SerpentsFangEffect")]
public class SerpentsFang : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Add poison effect to face
        face.effect = EffectType.Poison;
    }
}