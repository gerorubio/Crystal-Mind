using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts")]
public class ArtifactSO : ScriptableObject {
    public string artifactName;
    public string[] effects;
    public Sprite artWork;
    public ArtifactEffect artifactEffect;

    public bool onShoot;
    public bool onEquip;
    public bool onReload;
    public bool onSpellCast;
    public bool onDamageTaken;
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

}