using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellDatabase : ScriptableObject {
    public SpellSO[] spell;

    public int SpellCount {
        get {
            return spell.Length;
        }
    }

    public SpellSO GetSpell(int index) {
        return spell[index];
    }

}