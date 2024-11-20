using UnityEngine;

[CreateAssetMenu(fileName = "Orb Of Refraction", menuName = "Artifacts/Effects/OrbOfRefractionEffect")]
public class OrbOfRefraction : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // An orb rotates around you, and whenever a shot passes through it, 2 additional projectiles are generated.
        // TBI
        // Increase fire rate 10%
        player.CurrentFireRate += (player.CurrentFireRate * .1f);
    }
}