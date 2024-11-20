using UnityEngine;

[CreateAssetMenu(fileName = "Fool", menuName = "Artifacts/Effects/FoolEffect")]
public class Fool : ArtifactEffect {
    public override void OnReload(Character player, Weapon weapon) {
        // Add Xd1 when reloading, where X is the number of dice you have.
        for (int i = 0; i < weapon.AmmunitionSystem.Ammunition.Count; i++) {
            // Need to add d1 model
            //weapon.AmmunitionSystem.CurrentAmmunition.Add(new Dice(1));
        }
    }
}