using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum ArtifactName {
    Arsenic,
    BloodCatalyst,
    Campfire,
    CoinFlip,
    Contagion,
    CupidsArrow,
    DivineReload,
    EchoOfSparks,
    ElectricCharger,
    Fool,
    FrostbiteNova,
    GunpowderBoost,
    Guillotine,
    Hemomancy,
    HollowRecharge,
    Hourglass,
    InfernoPulse,
    LifeConduit,
    MagicReload,
    Madness,
    NaturesGift,
    ObsidianFury,
    OrbOfRefraction,
    Pact,
    PerfectGrip,
    Recycler,
    RubyRoulette,
    Runners,
    SeeingDouble,
    SerpentsFang,
    SnakeNest,
    Sniper,
    Spellbook,
    Static,
    StellarConduit,
    ThornVines,
    TitansReach,
    VenomPuddle,
    VoidCollapse,
    Whetstone,
    ZephyrsGale
}

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts")]
public class ArtifactSO : ScriptableObject {
    public string artifactName;
    public ArtifactName enumArtifactName;
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