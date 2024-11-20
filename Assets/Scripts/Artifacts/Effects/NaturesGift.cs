using UnityEngine;

[CreateAssetMenu(fileName = "Natures Gift", menuName = "Artifacts/Effects/NaturesGiftEffect")]
public class NaturesGift : ArtifactEffect {
    private bool avaliable = true;

    public override void OnTakeDamage(Character player) {
        if(player.CurrentHp == 0 && avaliable) {
            player.CurrentHp++;
            avaliable = false;
        }
    }
}