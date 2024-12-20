using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]
public class SpellSO : ScriptableObject {
    public string spellName;
    public string description;
    public Sprite artWork;
    
    public SpellEffect effect;

    public int cost;

    public void Cast() {

    }
}
