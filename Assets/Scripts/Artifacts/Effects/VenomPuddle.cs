using UnityEngine;

[CreateAssetMenu(fileName = "Venom Puddle", menuName = "Artifacts/Effects/VenomPuddleEffect")]
public class VenomPuddle: ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        //Poison decrease chance reduced by 30 %.
        // TBI
        // Add poison effect to face
        face.effect = EffectType.Poison;
    }
}