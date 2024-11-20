using UnityEngine;

[CreateAssetMenu(fileName = "Hemomancy", menuName = "Artifacts/Effects/HemomancyEffect")]
public class Hemomancy : ArtifactEffect {
    public override void OnSpellCast(Character player) {
        // 3 % chance to gain 1 health when using an active ability.
        if (Random.Range(0, 100) < 3) {
            player.CurrentHp++;
        }
    }
}