using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blizzard", menuName = "Spells/Effects/BlizzardEffect")]
public class Blizzard : SpellEffect {
    public GameObject blizzard;
    public override void Cast(Character player) {
        Instantiate(blizzard, player.transform.position, Quaternion.identity);
    }
}
