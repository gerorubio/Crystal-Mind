using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts")]
public class ArtifactSO : ScriptableObject {
    public string artifactName;
    public string[] effects;
    public Sprite artWork;

    public bool onShoot;
    public bool onEquip;
    public bool onReload;
    public bool onSpellCast;
    public bool OnDamageTaken;
    public bool onDash;
    public bool onAmmo;
    public bool onDeath;
    public bool onEnemyDeath;
    public bool onBleedingDamage;
    public bool onDiceModified;
    public bool onHit;

    public bool upgradeDice;
    public int value;
    public EffectType effect;

    // Effect:
    // Value multiplier (Gunpowder Boost, Spellbook, Stellar Conduit, Madness, Sniper)
    // Add status (Whetstone, Cupid's Arrow, Void Collapse, Titan's Reach)
    // Time base (Arsenic, Hourglass, Venom Puddle)
    // Value add  (Ruby Roulette, Serpent's Fang, Guillotine)
    // CreateObject (Orb of Refraction)
    // Stat increase (Campfire, Life Conduit, Runners, Snake Nest, Perfect Grip, Magic Reload, Zephyr's Gale, Divine Reload, Trash Can, Obsidian Fury, Nature's Gift, Pact, Hemomancy)
    // Modify ammunition (Seeing Double, Fool, Pact)
    // Modify spell (Echo Sparks, Blood Catalyst, Hollow Recharge)
    // Global (Static, Electric Charger, Inferno Pulse, Thron Vines)


    public void Shoot() {
        Debug.Log($"{artifactName} is shooting!");
    }

    public void Equip(Character player, Weapon weapon, Face face) {
        Debug.Log($"{artifactName} has been equipped!");

        switch (artifactName) {
            case "Arsenic":
                // Poison Tick Time  reduced by 50%
                // TBI
                // Add poison effect to face
                face.effect = EffectType.Poison;
                break;
            case "Blood Catalyst":
                // Add bleed effect to face
                face.effect = EffectType.Bleed;
                break;
            case "Campfire":
                // Increases collection range by 20%.
                player.CurrentXpCollectionRange += (player.CurrentXpCollectionRange * .2f);
                //Add alceanistum effect to face
                face.effect = EffectType.Alceanistum;
                break;
            case "Coin Flip":
                // 50% chance to either gain or lose 300 alceanistum.
                int gamble = 300;
                if (Random.Range(0, 2) == 0) {
                    gamble = -gamble;
                }
                player.GainXP(gamble);
                // Increases collection range by 10%.
                player.CurrentXpCollectionRange += (player.CurrentXpCollectionRange * .1f);
                break;
            case "Contagion":
                // Add poison effect to face
                face.effect = EffectType.Poison;
                break;
            case "Cupid's Arrow":
                // Grant piercing
                weapon.Piercing = true;
                // Add obsidian effectm to face
                face.effect = EffectType.Obsidian;
                break;
            case "Divine Reload":
                // Increase reload speed by 10%.
                player.CurrentReloadSpeed += (player.CurrentReloadSpeed * .1f);
                break;
            case "Electric Charger":
                // Increase reload speed by 10%.
                player.CurrentReloadSpeed += (player.CurrentReloadSpeed * .1f);
                break;
            case "Obsidian Fury":
                // Increase fire rate 10%
                player.CurrentFireRate += (player.CurrentFireRate * .1f);
                // Add obsidian effectm to face
                face.effect = EffectType.Obsidian;
                break;
            case "Orb of Refraction":
                // An orb rotates around you, and whenever a shot passes through it, 2 additional projectiles are generated.

                // Increase fire rate 10%
                player.CurrentFireRate += (player.CurrentFireRate * .1f);
                return;
            case "Pact":
                // Heal 5 health points, but lose 2 random dice.
                player.CurrentHp += 5;
                for(int i = 0; i < 2; i++) {
                    int randomDice = Random.Range(0, weapon.AmmunitionSystem.Ammunition.Count);
                    Dice selectDice = weapon.AmmunitionSystem.Ammunition[randomDice];

                    weapon.AmmunitionSystem.Ammunition.RemoveAt(randomDice);
                }
                break;
            case "Perfect Grip":
                // Increase fire rate 10%
                player.CurrentFireRate += (player.CurrentFireRate * .1f);
                break;
            case "Ruby Roulette":
                // Increase critical chance by 10%
                player.CurrentCriticalChance += (player.CurrentCriticalChance * .1f);
                break;
            case "Runners":
                // Increase movement speed by 10%
                player.CurrentMovementSpeed += (player.CurrentMovementSpeed * .1f);
                break;
            case "Seeing Double":
                // Increase fire rate 10%
                player.CurrentFireRate += (player.CurrentFireRate * .1f);
                break;
            case "Serpent's Fang":
                // Add poison effect to face
                face.effect = EffectType.Poison;
                break;
            case "Snake Nest":
                // Change value of a face to 2
                face.value = 2;
                break;
            case "Sniper":
                // Increase attack range 10%
                player.CurrentAttackRange += (player.CurrentAttackRange * .2f);
                break;
            case "Static":
                // Increase fire rate 10%
                player.CurrentFireRate += (player.CurrentFireRate * .1f);
                break;
            case "Thorn Vines":
                // Increase 1 HP
                player.CurrentHp += 1;
                break;
            case "Titan's Reach":
                // Increase projectile size 10%
                weapon.ProjectileSize += (weapon.ProjectileSize * .1f);
                // Increase projectile size 10%
                weapon.Knockback += (weapon.Knockback * .1f);
                break;
            case "Recycler":
                // Increase reload speed by 10%.
                player.CurrentReloadSpeed += (player.CurrentReloadSpeed * .1f);
                break;
            case "Venom Puddle":
                // Add poison effect to face
                face.effect = EffectType.Poison;
                break;
            case "Void Collapse":
                // Change value of a face to 1
                face.value = 1;
                break;
            case "Whetstone":
                // Change value of a face to 2
                face.value = 2;
                break;
            case "Zephyr's Gale":
                // Increase movement speed by 10%
                player.CurrentMovementSpeed += (player.CurrentMovementSpeed * .1f);
                break;
            default: break;
        }

        return;
    }

    public void Reload() {
        Debug.Log($"{artifactName} has been equipped!");
    }

    public void SpellCast() {

    }

    public void TakingDamage() {
        
    }

    public void Dash() {

    }

}