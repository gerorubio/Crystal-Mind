using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice {
    public string type;
    private Face[] faces;
    public Face currentFace;

    public Dice(int numberOfFaces) {
        // Initialize the faces array with the specified length d4, d6, d8, d10, d12, d20
        this.faces = new Face[numberOfFaces];

        for (int i = 0; i < numberOfFaces; i++) {
            this.faces[i] = new Face(i + 1, "None");
        }

        switch (numberOfFaces) {
            case 4:
                type = "d4";
                break;
            case 6:
                type = "d6";
                break;
            case 8:
                type = "d8";
                break;
            case 10:
                type = "d10";
                break;
            case 12:
                type = "d12";
                break;
            case 20:
                type = "d20";
                break;
            default:
                Debug.LogError("Number of faces not allowed");
                break;
        }

    }

    public void ModifyValue(int faceIndex, int newValue) {
        if (faceIndex >= 0 && faceIndex < faces.Length) {
            this.faces[faceIndex].value = newValue;
        } else {
            Debug.LogError("Face index out of bounds.");
        }
    }

    public void AddEffect(int faceIndex, string newEffect) {
        if (faceIndex >= 0 && faceIndex < faces.Length) {
            this.faces[faceIndex].effect = newEffect;
        } else {
            Debug.LogError("Face index out of bounds.");
        }
    }

    public void PrintAllFaces() {
        for (int i = 0; i < faces.Length; i++) {
            Debug.Log("Face " + (i + 1) + ": Value = " + faces[i].value + ", Effect = " + faces[i].effect);
        }
    }

    public void RollDice() {
        currentFace = this.faces[UnityEngine.Random.Range(0, this.faces.Length)];
    }


    public int SelectFace() {
        int index = UnityEngine.Random.Range(0, this.faces.Length);

        return index;
    }
}
