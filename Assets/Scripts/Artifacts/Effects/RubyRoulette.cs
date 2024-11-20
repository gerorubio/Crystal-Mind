using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ruby Roulette", menuName = "Artifacts/Effects/RubyRouletteEffect")]
public class RubyRoulette: ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increase critical chance by 10%
        player.CurrentCriticalChance += (player.CurrentCriticalChance * .1f);
    }
}