using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SnakeEyes", menuName = "Spells/Effects/SnakeEyesEffect")]
public class SnakeEyes : SpellEffect {
    public override void Cast(Character player) {
        for (int i = 0; i < 6; i++) {
            player.Weapon.AmmunitionSystem.CurrentAmmunition.Add(new Dice(4, 1, EffectType.Bleed));
        }
    }
}
