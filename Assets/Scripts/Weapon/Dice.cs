using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice {
    private int[] faces;
    private string[] effect;

    public Dice(int numberOfFaces) {
        // Initialize the faces array with the specified length d4, d6, d8, d10, d12, d20
        this.faces = new int[numberOfFaces];
        this.effect = new string[numberOfFaces];

        for (int i = 0; i < numberOfFaces; i++) {
            this.faces[i] = i + 1;
        }
    }

    public void ModifyValue(int faceIndex, int newValue) {
        if (faceIndex >= 0 && faceIndex < faces.Length) {
            this.faces[faceIndex] = newValue;
        } else {
            Debug.LogError("Face index out of bounds.");
        }
    }

    public void AddEffect(int faceIndex, string newEffect) {
        if (faceIndex >= 0 && faceIndex < effect.Length) {
            this.effect[faceIndex] = newEffect;
        } else {
            Debug.LogError("Face index out of bounds.");
        }
    }
}
