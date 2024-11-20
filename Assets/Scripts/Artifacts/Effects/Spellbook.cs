using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spellbook", menuName = "Artifacts/Effects/SpellbookEffect")]
public class Spellbook : ArtifactEffect {

    public override void OnReload(Character player, Weapon weapon) {
        // Each die’s value is doubled and added to the spell’s recharge amount.
        // TBI
    }
}