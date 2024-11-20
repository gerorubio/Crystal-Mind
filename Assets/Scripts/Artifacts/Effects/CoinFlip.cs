using UnityEngine;

[CreateAssetMenu(fileName = "Coin Flip", menuName = "Artifacts/Effects/CoinFlipEffect")]
public class CoinFlip : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // 50% chance to either gain or lose 300 alceanistum.
        int gamble = 300;
        if (Random.Range(0, 2) == 0) {
            gamble = -gamble;
        }
        player.GainXP(gamble);
        // Increases collection range by 10%.
        player.CurrentXpCollectionRange += (player.CurrentXpCollectionRange * .1f);
    }
}