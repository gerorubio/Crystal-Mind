using UnityEngine;

public enum EffectType {
    None,
    Burned,
    Frozen,
    Bleed,
    Poison,
    Obsidian,
    Alceanistum,
    Broken
}

public class Face {
    public int value;
    public EffectType effect;

    public Face(int value, EffectType effect) {
        this.value = value;
        this.effect = effect;
    }

    public static EffectType GetRandomEffect() {
        // 25% chance to have an effect
        if (Random.value < 0.25f) {
            // Get all EffectType values except None
            EffectType[] possibleEffects = new EffectType[] {
                EffectType.Burned,
                EffectType.Frozen,
                EffectType.Bleed,
                EffectType.Poison,
                EffectType.Obsidian,
                EffectType.Alceanistum,
            };

            // Return a random EffectType from the list
            int randomIndex = Random.Range(0, possibleEffects.Length);
            return possibleEffects[randomIndex];
        }

        // Otherwise, no effect
        return EffectType.None;
    }
}
