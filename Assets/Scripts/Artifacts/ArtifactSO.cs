using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts")]
public class ArtifactSO : ScriptableObject {
    public string artifactName;
    public string description;
    public Sprite artWork;

    public bool onShoot;
    public bool onEquip;
    public bool onReload;
    public bool onSpellCast;
    public bool onTakingDamage;
    public bool onDash;
    public bool onAmmo;
    public bool onDeath;

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

    public void OnEquip() {
        Debug.Log($"{artifactName} has been equipped!");

        //switch(artifactName) {
        //    case ""
        //}
    }

    public void OnReload() {
        Debug.Log($"{artifactName} has been equipped!");
    }

    public void OnSpellCast() {

    }

    public void OnTakingDamage() {
        
    }

    public void OnDash() {

    }

}