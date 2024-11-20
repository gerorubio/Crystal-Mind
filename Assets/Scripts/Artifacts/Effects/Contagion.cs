using UnityEngine;

[CreateAssetMenu(fileName = "Contagion", menuName = "Artifacts/Effects/ContagionEffect")]
public class Contagion : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Add poison effect to face
        face.effect = EffectType.Poison;
    }
}