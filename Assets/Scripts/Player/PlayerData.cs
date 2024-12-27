using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int currentLevel;
    public int xpToNextLevel;
    public int currentXp;
    public int currentHp;
    public float[] position;

    public string equippedSpell;
    public List<string> artifacts;

    public PlayerData(Character player) {
        currentLevel = player.CurrentLevel;
        currentHp = player.CurrentHp;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        // Save ScriptableObjects as identifiers
        equippedSpell = player.CurrentSpell != null ? player.CurrentSpell.name : null;

        Debug.Log(equippedSpell);

        artifacts = new List<string>();
        foreach (var artifact in player.GetArtifacts()) {
            artifacts.Add(artifact.name);
        }

        Debug.Log(artifacts);
    }
}
