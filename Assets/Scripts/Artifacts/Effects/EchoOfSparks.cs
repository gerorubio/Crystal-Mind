using UnityEngine;

[CreateAssetMenu(fileName = "Echo Of Sparks", menuName = "Artifacts/Effects/EchoOfSparksEffect")]
public class EchoOfSparks : ArtifactEffect {
    public override void OnReload(Character player, Weapon weapon) {
        // Restores 10% of your spell points when reloading.
        // TBI
    }
}