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


    public void Shoot() {
        Debug.Log($"{artifactName} is shooting!");
    }

    public void OnEquip() {
        Debug.Log($"{artifactName} has been equipped!");
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