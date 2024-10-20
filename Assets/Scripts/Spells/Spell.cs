using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    public SpellSO spellToCast;

    private void Awake() {
        Debug.Log(spellToCast.spellName);
    }
}
