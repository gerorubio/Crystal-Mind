using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType {
    None,
    Burned,
    Frozen,
    Bleed,
    Poison,
    Obsidian,
    Alceanistum
}

public class Face {
    public int value;
    public EffectType effect;

    public Face(int value, EffectType effect) {
        this.value = value;
        this.effect = effect;
    }
}
