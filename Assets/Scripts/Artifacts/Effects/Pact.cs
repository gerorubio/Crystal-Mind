using UnityEngine;

[CreateAssetMenu(fileName = "Pact", menuName = "Artifacts/Effects/PactEffect")]
public class Pact : ArtifactEffect {
    public override void OnEquip(Character player, Weapon weapon, Face face) {
        // Heal 5 health points, but lose 2 random dice.
        player.CurrentHp += 5;
        for (int i = 0; i < 2; i++) {
            int randomDice = Random.Range(0, weapon.AmmunitionSystem.Ammunition.Count);
            weapon.AmmunitionSystem.Ammunition.RemoveAt(randomDice);
        }
    }
}