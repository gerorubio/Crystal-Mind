using UnityEngine;

[CreateAssetMenu(fileName = "HollowRecharge", menuName = "Artifacts/Effects/HollowRechargeEffect")]
public class HollowRecharge : ArtifactEffect {
    public override void OnReload(Character player, Weapon weapon) {
        // Reloading an empty cartridge grants X charge to the active ability, where X is the number of dice.
        // TBI
    }
}