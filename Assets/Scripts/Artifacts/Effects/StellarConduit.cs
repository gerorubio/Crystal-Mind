using UnityEngine;

[CreateAssetMenu(fileName = "Stellar Conduit", menuName = "Artifacts/Effects/StellarConduitEffect")]
public class StellarConduit: ArtifactEffect {

    public override void OnReload(Character player, Weapon weapon) {
        // Each time an even number contributes to spell progress, unleash a damage wave equal to three times the sum of all even dice.
        // TBI
    }
}