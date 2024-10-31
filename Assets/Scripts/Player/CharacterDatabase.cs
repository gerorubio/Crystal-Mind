using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject {
    public CharacterSO[] character;

    public int CharacterCount {
        get {
            return character.Length;
        }
    }

    public CharacterSO GetCharacter(int index) {
        return character[index];
    }
}
