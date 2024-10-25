using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArtifactDatabase : ScriptableObject {
    public ArtifactSO[] artifact;

    public int ArtifactCount {
        get {
            return artifact.Length;
        }
    }

    public ArtifactSO GetArtifact(int index) {
        return artifact[index];
    }

}