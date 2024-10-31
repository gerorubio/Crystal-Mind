using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {
    public CharacterDatabase characterDb;
    public Sprite[] dices;

    // Character
    public TextMeshProUGUI nameText;
    public Image wantedPoster;
    public TextMeshProUGUI descriptionText;
    public Image[] diceSet;
    public Image spell;
    public Image artifact;

    private int selectedOption = 0;

    private void Start() {
        selectedOption = PlayerPrefs.HasKey("selectedOption") ? PlayerPrefs.GetInt("selectedOption") : 0;
        UpdateCharacter(selectedOption);
    }

    public void NextOption() {
        selectedOption = (selectedOption + 1) % characterDb.CharacterCount;
        UpdateCharacter(selectedOption);
        Save();
    }

    public void PreviousOption() {
        selectedOption = (selectedOption - 1 + characterDb.CharacterCount) % characterDb.CharacterCount;
        UpdateCharacter(selectedOption);
        Save();
    }

    public void UpdateCharacter(int selectedOption) {
        CharacterSO character = characterDb.GetCharacter(selectedOption);
        // Wanted Poster
        wantedPoster.sprite = character.characterSprite;
        // Name character
        nameText.text = character.characterName;
        // Description
        descriptionText.text = character.description;
        // Dice Set
        int[] diceCounts = { character.d4, character.d6, character.d8, character.d10, character.d12, character.d20 };

        ClearDiceSet();

        int diceIndex = 0;
        for (int diceType = 0; diceType < diceCounts.Length; diceType++) {
            for (int j = 0; j < diceCounts[diceType]; j++) {
                if (diceIndex < diceSet.Length) {
                    diceSet[diceIndex].sprite = dices[diceType];
                    diceIndex++;
                }
            }
        }

        // Spell
        spell.sprite = character.initialSpell.artWork;
        // Artifact
        artifact.sprite = character.initialArtifact.artWork;
    }

    private void ClearDiceSet() {
        foreach (var dice in diceSet) {
            dice.sprite = null;
        }
    }

    private void Load() {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save() {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
}
