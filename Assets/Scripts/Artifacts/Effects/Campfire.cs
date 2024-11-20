using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Campfire", menuName = "Artifacts/Effects/CampfireEffect")]
public class Campfire : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Increases collection range by 20%.
        player.CurrentXpCollectionRange += (player.CurrentXpCollectionRange * .2f);
        // Add alceanistum effect to face
        face.effect = EffectType.Alceanistum;
    }
}