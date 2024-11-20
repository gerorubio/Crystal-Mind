using UnityEngine;

[CreateAssetMenu(fileName = "Life Conduit", menuName = "Artifacts/Effects/LifeConduitEffect")]
public class LifeConduit: ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        //Add 1 burned face to a dice
        player.CurrentHp++;
    }
}