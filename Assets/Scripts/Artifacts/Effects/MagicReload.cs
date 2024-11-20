using UnityEngine;

[CreateAssetMenu(fileName = "Magic Reload", menuName = "Artifacts/Effects/MagicReloadEffect")]
public class MagicReload : ArtifactEffect {

    public override void OnSpellCast(Character player) {
        // Each time you use an active ability, increase the reload speed of your next reload by 25%.
        // TBI
    }
}