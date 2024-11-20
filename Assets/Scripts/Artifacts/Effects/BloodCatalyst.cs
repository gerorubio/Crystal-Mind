using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blood Catalyst", menuName = "Artifacts/Effects/CampfireEffect")]
public class BloodCatalyst : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Add bleed effect to face
        face.effect = EffectType.Bleed;
    }
}