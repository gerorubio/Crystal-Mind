using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadedDice", menuName = "Spells/Effects/LoadedDiceEffect")]
public class LoadedDice : SpellEffect {
    public override void Cast(Character player) {
        AmmunitionSystem ammunitionSystem = player.Weapon.AmmunitionSystem;

        ammunitionSystem.Reload();

        foreach (Dice dice in ammunitionSystem.CurrentAmmunition) {
            dice.SetToMaxFace();
        }
    }
}
