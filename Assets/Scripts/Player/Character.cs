using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character {
    public string characterName;
    public Sprite characterSprite;
    public string description;
    public int d4;
    public int d6;
    public int d8;
    public int d10;
    public int d12;
    public int d20;

    public SpellSO initialSpell;
    public ArtifactSO initialArtifact;
}
