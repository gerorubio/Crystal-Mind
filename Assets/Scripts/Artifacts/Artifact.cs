using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour {
    public ArtifactSO equippedArtifact;

    public void OnEquip() {
        Debug.Log(equippedArtifact.artifactName);
    }
}
