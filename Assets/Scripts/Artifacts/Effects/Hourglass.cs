using UnityEngine;

[CreateAssetMenu(fileName = "Hourglass", menuName = "Artifacts/Effects/HourglassEffect")]
public class Hourglass : ArtifactEffect {

    public override void OnSpellCast(Character player) {
        // 10% chance to trigger the spell twice.
        if (Random.Range(0, 100) < 10) {
            player.CurrentSpell.effect.Cast(player);
        }
    }
}