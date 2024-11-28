using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioClipEntry {
    public string key;
    public AudioClip value;
}

public class DicesSFXHandler : MonoBehaviour {
    public AudioSource shuffleAudioSource;
    public List<AudioSource> diceAudioSources;
    // Shuffle audios
    public List<AudioClip> shuffleAudiosList;
    // Roll audios
    public List<AudioClipEntry> rollAudiosList;
    // Dictionary to get adios by key
    private Dictionary<string, AudioClip> rollAudios;

    // Singleton
    public static DicesSFXHandler Instance { get; private set; }

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        // Convert List to dictionary for rollAudios
        rollAudios = new Dictionary<string, AudioClip>();

        foreach(AudioClipEntry entry in rollAudiosList) {
            rollAudios[entry.key] = entry.value;
        }
    }

    public void ShuffleDice() {
        shuffleAudioSource.clip = shuffleAudiosList[Random.Range(0, shuffleAudiosList.Count)];
        shuffleAudioSource.Play();
    }

    public void StopShuffleDice() {
        shuffleAudioSource.Stop();
    }

    public void RollDice(List<Dice> ammunition) {
        Dictionary<string, int> diceCounts = new Dictionary<string, int> {
            { "d4", 0 },
            { "d6", 0 },
            { "d8", 0 },
            { "d10", 0 },
            { "d12", 0 },
            { "d20", 0 }
        };

        foreach (Dice dice in ammunition) {
            if(diceCounts.ContainsKey(dice.type)) {
                diceCounts[dice.type]++;
            }
        }

        string[] diceTypes = { "d4", "d6", "d8", "d10", "d12", "d20" };

        for (int i = 0; i < diceTypes.Length; i++) {
            string diceType = diceTypes[i];
            int count = diceCounts[diceType];
            AudioSource source = diceAudioSources[i];

            if (count == 0) {
                source.clip = null;
            } else if (count >= 10) {
                source.clip = rollAudios[$"10{diceType}"];
            } else {
                source.clip = rollAudios[$"{count}{diceType}"];
            }

            if(source.clip != null) {
                source.Play();
            }
        }

    }
}