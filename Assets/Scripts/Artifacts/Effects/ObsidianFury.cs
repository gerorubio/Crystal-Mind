using UnityEngine;

[CreateAssetMenu(fileName = "Obsidian Fury", menuName = "Artifacts/Effects/ObsidianFuryEffect")]
public class ObsidianFury : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase fire rate 10%
        player.CurrentFireRate += (player.CurrentFireRate * .1f);
        // Add obsidian effect to face
        face.effect = EffectType.Obsidian;
        // Increases fire rate by 2% for each broken obsidian face.
        foreach(Dice dice in weapon.AmmunitionSystem.CurrentAmmunition) {
            foreach(Face jface in dice.Faces) {
                if(jface.effect == EffectType.Broken) {
                    weapon.FireRate += (weapon.FireRate * 0.02f);
                }
            }
        }
    }
}