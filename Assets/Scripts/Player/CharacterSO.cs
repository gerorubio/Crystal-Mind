using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters")]
public class CharacterSO : ScriptableObject {
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

    public int hp = 3;
    public float reloadSpeed = 2f;
    public float fireRate = 2f;
    public float movementSpeed = 10f;
    public float attackRange = 10f;
}
