using UnityEngine;

[CreateAssetMenu(fileName = "Inferno Pulse", menuName = "Artifacts/Effects/InfernoPulseEffect")]
public class InfernoPulse: ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        //Add 1 burned face to a dice
        face.effect = EffectType.Burned;
    }

    public override void OnSpellCast(Character player) {
        // Deal 10 damage to all enemies when using an active ability.
        // TBI
    }
}